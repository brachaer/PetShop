using Microsoft.EntityFrameworkCore;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;

namespace PetShop.Data.Repositories
{
	public class AnimalRepository : IRepository<Animal>
	{
		private readonly PetShopDbContext _context;
		private readonly IRepository<Category> _categoryRepository;

		public AnimalRepository(PetShopDbContext context, IRepository<Category> categoryRepository)
		{
			_context = context;
			_categoryRepository = categoryRepository;
		}
		public async Task<Animal> Get(int id)
		{
			var animal = await _context.Animals.Include(a => a.Comments).SingleOrDefaultAsync(a => a.Id == id);
			return animal;
		}
		public async Task<PagedDataResponse<Animal>> Get(PagedDataRequest request)
		{
			var filter = ValidatePagedRequest(request);

			var data = await _context.Animals.Where(a => filter.CategoryId == null || a.CategoryId == filter.CategoryId)
				.Include(a => a.Comments)
				.OrderByDescending(a => a.Comments.Count)
				.Skip(filter.Skip)
				.Take(filter.PageSize)
				.ToListAsync();
			var count = _context.Animals.Count(a => filter.CategoryId == null || a.CategoryId == filter.CategoryId);

			return new PagedDataResponse<Animal>
			{
				PageSize = filter.PageSize,
				PageNumber = filter.PageNumber,
				Data = data,
				TotalCount = count
			};
		}
		public async Task<IEnumerable<Animal>> GetAll()
		{
			var animals = await _context.Animals.Include(a => a.Comments).OrderByDescending(a => a.Comments.Count).ToListAsync();
			return animals;
		}

		public async Task<int> Add(Animal item)
		{
			if (item == null)
				throw new ArgumentNullException();
			var id = _context.GetIdentityForNewEntry<Animal>();
			item.Id = id;
			AssignCommentsIds(item);
			await SetCategory(item);
			var entry = await _context.Animals.AddAsync(item);
			_context.SaveChangesWithIdentityInsert<Animal>();
			return entry.Entity.Id;
		}

		public async Task<Animal> Update(Animal item)
		{
			var entry = _context.Animals.Update(item);
			
			await _context.SaveChangesAsync();
			return entry.Entity;
		}
		public async Task<Animal> Delete(int id)
		{
			var animal = await Get(id);
			if (animal != null)
			{
				var entry = _context.Animals.Remove(animal);
				_context.SaveChanges();
				return entry.Entity;
			}
			return null;
		}
		private AnimalFilterRequest ValidatePagedRequest(PagedDataRequest request)
		{
			;
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

			if (request is AnimalFilterRequest filter)
				return filter;

			return new AnimalFilterRequest(request);
		}
		private void AssignCommentsIds(Animal item)
		{
			foreach (var comment in item.Comments)
			{
				if (comment.Id.Equals(default))
				{
					var commentId = _context.GetIdentityForNewEntry<Comment>();
					comment.Id = commentId;
				}
			}
			_context.SaveChangesWithIdentityInsert<Comment>();
		}
		private async Task SetCategory(Animal item)
		{
			var category = await _categoryRepository.Get(item.CategoryId);

			if (item.CategoryId.Equals(default) || category == null)
				throw new Exception();
			item.Category = category;
		}

	}
}
