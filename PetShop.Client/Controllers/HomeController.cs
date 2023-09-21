using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Model.Entities;
using PetShop.Services;
using System.Diagnostics;

namespace PetShop.Client.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPetShopService<Animal> _animalService;

		public HomeController(IPetShopService<Animal> animalsService)
		{
			_animalService = animalsService;
		}

		public async Task<IActionResult> Index()
		{
			var animals = await _animalService.GetAll();
			animals = animals.Take(2);
			return View(animals);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}