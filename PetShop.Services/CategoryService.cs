using Microsoft.AspNetCore.Http;
using PetShop.Data.Repositories;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;

namespace PetShop.Services
{
	public class CategoryService : IPetShopService<Category>
	{
		private readonly IRepository<Category> _categoryRepository;

		public CategoryService(IRepository<Category> categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}
		public async Task<int> Add(Category category)
		{
			await _categoryRepository.Add(category);
			return category.Id;
		}

		public async Task<Category> Get(int categoryId)
		{
			var category = await _categoryRepository.Get(categoryId);
			return category;
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			return await _categoryRepository.GetAll();
		}
		public async Task<PagedDataResponse<Category>> Get(PagedDataRequest request)
		{
			return await _categoryRepository.Get(request);
		}
		public async Task<Category> Delete(int id)
		{
			return await _categoryRepository.Delete(id);
		}

		public async Task<Category> Update(Category item)
		{
			return await _categoryRepository.Update(item);
		}

		public async Task<int> Add(Category item, IFormFile formFile = null)
		{
			await _categoryRepository.Add(item);
			return item.Id;
		}

		public async Task<Category> Update(Category item, IFormFile formFile = null)
		{
			return await _categoryRepository.Update(item);
		}
	}
}
