using Microsoft.AspNetCore.Mvc;
using PetShop.Model.Entities;
using PetShop.Services;

namespace PetShop.Client.Controllers
{
	public class AnimalController : Controller
	{
		private readonly IPetShopService<Animal> _animalService;
		private readonly IPetShopService<Category> _categoryService;

		public AnimalController(IPetShopService<Animal> animalService, IPetShopService<Category> categoryService) 
		{
			_animalService = animalService;
			_categoryService = categoryService;
		}
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var animal = await GetAnimal(id);
			ViewData["CategoryName"] = await GetCategoryName(id);
			return View(animal);
		}
		[HttpPost]
		public async Task<IActionResult> Details(int id, string userName, string content)
		{
			var animal = await GetAnimal(id);
			var comment = CreateComment(ref userName, content, animal);
			if (ModelState.IsValid)
			{
				animal.Comments.Add(comment);
			}

			await _animalService.Update(animal);
			ViewData["CategoryName"] = await GetCategoryName(id);
			return View(animal);
		}

		private static Comment CreateComment(ref string userName, string content, Animal animal)
		{
			if (userName == null || userName == string.Empty)
				userName = "Anonymous";
			var comment = new Comment
			{
				UserName = userName,
				Content = content,
				CommentDate = DateTime.Now,
				AnimalId = animal.Id
			};
			return comment;
		}

		private async Task<Animal> GetAnimal(int id)
		{
			var animal = await _animalService.Get(id);
			return animal;
		}
		private async Task<string> GetCategoryName(int id)
		{
			var animal = await GetAnimal(id);
			var categoryId = animal.CategoryId;
			var category= await _categoryService.Get(categoryId);
			return category.Name;
		}

	}
}
