using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;
using PetShop.Services;

namespace PetShop.Client.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IPetShopService<Animal> _animalService;
		private readonly IPetShopService<Category> _categoryService;

		public AdminController(IPetShopService<Animal> animalService, IPetShopService<Category> categoryService)
		{
			_animalService = animalService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var request = new AnimalFilterRequest();

			return await DisplayCatalog(request);
		}
		[HttpPost]
		public async Task<IActionResult> Index(AnimalFilterRequest request)
		{
			return await DisplayCatalog(request);
		}

		[HttpGet]
		public async Task<IActionResult> AddAnimal()
		{
			ViewData["Category"] = await GetCategories();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddAnimal(Animal model, IFormFile imageFile)
		{
			if (ModelState.IsValid)
			{
				await _animalService.Add(model, imageFile);
				return RedirectToAction("Index");
			}
			ViewData["Category"] = GetCategories();
			return View(model);
		}

		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.Add(category);
				return RedirectToAction("Index");
			}
			return View(category);
		}

		[HttpGet]
		public async Task<IActionResult> EditAnimal(int id)
		{
			ViewData["Category"] = await GetCategories();
			var animal = await GetAnimal(id);
			return View(animal);
		}

		[HttpPost]
		public async Task<IActionResult> EditAnimal(Animal model, IFormFile imageFile)
		{
			if (ModelState.IsValid)
			{
				await _animalService.Update(model, imageFile);
				return RedirectToAction("Index");
			}

			ViewData["Category"] = await GetCategories();
			return View(model);
		}

		[HttpGet]
		public IActionResult DeleteAnimal()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAnimal(int id)
		{
			var animal = await GetAnimal(id);
			if (animal != null && Request.Form["confirmDelete"] == "Yes")
			{
				await _animalService.Delete(id);
			}
			return RedirectToAction("Index");
		}
		private async Task<IActionResult> DisplayCatalog(AnimalFilterRequest request)
		{
			ViewData["Title"] = "Pet Shop List";
			var response = await _animalService.Get(request);
			ViewData["Category"] = await GetCategories();
			ViewData["CurrentCategory"] = request.CategoryId;
			ViewData["request"] = request;
			ViewData["response"] = response;
			return View("Index", response.Data);
		}

		private async Task<IEnumerable<Category>> GetCategories()
		{
			var categories = await _categoryService.GetAll();
			return categories;
		}
		private async Task<Animal> GetAnimal(int id)
		{
			var animal = await _animalService.Get(id);
			return animal;
		}
	}
}
