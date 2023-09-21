using Microsoft.EntityFrameworkCore;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;

namespace PetShop.Data.Repositories
{
	public class CategoryRepository : IRepository<Category>
	{
		private readonly PetShopDbContext _context;
		public CategoryRepository(PetShopDbContext context)
		{
			_context = context;
		}
		public async Task<Category> Get(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			return category;
		}

		public async Task<PagedDataResponse<Category>> Get(PagedDataRequest request)
		{
			request = ValidatePagedRequest(request);
			var data = await _context.Categories
				.Skip(request.Skip)
				.Take(request.PageSize)
				.ToListAsync();

			var count = _context.Categories.Count();
			return new PagedDataResponse<Category>
			{
				PageSize = request.PageSize,
				PageNumber = request.PageNumber,
				Data = data,
				TotalCount = count
			};
		}
		public async Task<IEnumerable<Category>> GetAll()
		{
			var categories = await _context.Categories.ToListAsync();

				return categories;
		}
		public async Task<int> Add(Category item)
		{
			if (item == null)
				throw new ArgumentNullException();
			var id = _context.GetIdentityForNewEntry<Category>();
			item.Id = id;
			var entry = await _context.Categories.AddAsync(item);
			 _context.SaveChangesWithIdentityInsert<Category>();
			return id;
		}
		public async Task <Category> Update(Category item)
		{
			var entry = _context.Categories.Update(item);
			await _context.SaveChangesAsync();

			return entry.Entity;
		}
		public async Task <Category> Delete(int id)
		{
			var category = await Get(id);
			if (category != null)
			{
				var entry = _context.Categories.Remove(category);
				_context.SaveChanges();
				return entry.Entity;
			}
			return null;
		}

		private PagedDataRequest ValidatePagedRequest(PagedDataRequest request)
		{
			if (request == null)
			{
				request = new PagedDataRequest();
			}
			if (request.PageSize < 1)
			{
				request.PageSize = 10;
			}
			if (request.PageNumber < 1)
			{
				request.PageNumber = 1;
			}
			return request;
		}

	}
}
