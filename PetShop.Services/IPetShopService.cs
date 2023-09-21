using Microsoft.AspNetCore.Http;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;

namespace PetShop.Services
{
	public interface IPetShopService<T> where T : class
	{
		Task<T> Get(int id);
		Task<PagedDataResponse<T>> Get(PagedDataRequest request);
		Task<IEnumerable<T>> GetAll();
		Task<int> Add(T item, IFormFile formFile= null);
		Task<T> Update(T item, IFormFile formFile = null);
		Task<T> Delete(int id);
		//Task<int> AddAnimal(Animal model, IFormFile formFile);
		//Task<PagedDataResponse<Animal>> GetAnimals(PagedDataRequest request);
		//Task<IEnumerable<Animal>> GetAnimals();
		//Task<Animal> GetAnimalById(int id);
		//Task UpdateAnimal(Animal animal);
		//Task UpdateAnimal(Animal animal, IFormFile formFile);
		//Task DeleteAnimal(int id);
		//Task<int> AddCategory(Category category);
		//Task<string> GetCategoryName(int categoryId);
		//Task<IEnumerable<Category>> GetCategories();

	}
}
