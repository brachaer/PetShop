using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using PetShop.Data.Repositories;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;

namespace PetShop.Services
{
	public class AnimalsService : IPetShopService<Animal>
	{
		private readonly IRepository<Animal> _animalRepository;
		private readonly IPetShopService<Category> _categoryService;
		private readonly IMemoryCache _cache;
		private readonly ImageService _imageService;

		public AnimalsService(IRepository<Animal> animalRepository, IPetShopService<Category> categoryService, IMemoryCache cache, ImageService imageService)
		{
			_animalRepository = animalRepository;
			_categoryService = categoryService;
			_cache = cache;
			_imageService = imageService;
		}
		//public async Task<int> AddAnimal(Animal model, IFormFile formFile)
		//{
		//	var fileName = await InsertImage(model, formFile);
		//	if (fileName != null)
		//	{
		//		model.ImageUrl = fileName;
		//	}
		//	var id = await _animalRepository.Add(model);
		//	return id;
		//}

		//public async Task<PagedDataResponse<Animal>> GetAnimals(PagedDataRequest request)
		//{
		//	PagedDataResponse<Animal> response;

		//	if (!_cache.TryGetValue(request.ToString(), out response))
		//	{
		//		response = await _animalRepository.Get(request);
		//		_cache.Set(request.ToString(), response, TimeSpan.FromSeconds(20));
		//	}
		//	return response;
		//}
		//public async Task<IEnumerable<Animal>> GetAnimals()
		//{
		//	return await _animalRepository.GetAll();
		//}
		//public async Task<Animal> GetAnimalById(int id)
		//{
		//	var animal = await _animalRepository.Get(id);
		//	return animal;
		//}

		//public async Task UpdateAnimal(Animal animal)
		//{
		//	await _animalRepository.Update(animal);
		//}

		//private async Task UpdateAnimal(Animal animal, IFormFile formFile)
		//{
		//	if (formFile != null)
		//	{
		//		//var fileName = await InsertImage(animal, formFile);
		//		animal.ImageUrl = fileName;
		//	}
		//	await _animalRepository.Update(animal);
		//}
		public async Task<Animal> Delete(int id)
		{
			var animal = await Get(id);
			var fileName = animal.ImageUrl;
			_imageService.RemoveImage(fileName);
			return await _animalRepository.Delete(id);
		}

		public async Task<Animal> Get(int id)
		{
			var animal = await _animalRepository.Get(id);
			return animal;
		}

		public async Task<PagedDataResponse<Animal>> Get(PagedDataRequest request)
		{
			PagedDataResponse<Animal> response;
			if (!_cache.TryGetValue(request.ToString(), out response))
			{
				response = await _animalRepository.Get(request);
				_cache.Set(request.ToString(), response, TimeSpan.FromSeconds(20));
			}
			return response;
		}

		public async Task<IEnumerable<Animal>> GetAll()
		{
			return await _animalRepository.GetAll();
		}

		public async Task<int> Add(Animal item, IFormFile formFile)
		{
			var fileName = await _imageService.InsertImage(item, formFile);
			if (fileName != null)
			{
				item.ImageUrl = fileName;
			}
			var id = await _animalRepository.Add(item);
			return id;
		}

		public async Task<Animal> Update(Animal item, IFormFile formFile)
		{
			if (formFile != null)
			{
				var fileName = item.ImageUrl;
				_imageService.RemoveImage(fileName);
				 fileName = await _imageService.InsertImage(item, formFile);
				item.ImageUrl = fileName;
			}
			return await _animalRepository.Update(item);
		}


	}
}
