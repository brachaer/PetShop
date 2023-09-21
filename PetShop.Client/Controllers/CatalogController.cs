using Microsoft.AspNetCore.Mvc;
using PetShop.Model.Entities;
using PetShop.Model.Utilities;
using PetShop.Services;

namespace PetShop.Client.Controllers
{
	public class CatalogController : Controller
	{
		private readonly IPetShopService<Animal> _animalsService;
		private readonly IPetShopService<Category> _categoryService;

		public CatalogController(IPetShopService<Animal> animalsService, IPetShopService<Category> categoryService)
		{
			_animalsService = animalsService;
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

		private async Task<IActionResult> DisplayCatalog(AnimalFilterRequest request)
		{

			ViewData["Title"] = "Pet Shop List";
			var response = await _animalsService.Get(request);
			ViewData["Category"] = await _categoryService.GetAll();
			ViewData["CurrentCategory"] = request.CategoryId;
			ViewData["request"] = request;
			ViewData["response"] = response;
			return View("Index", response.Data);
		}


	}
}
