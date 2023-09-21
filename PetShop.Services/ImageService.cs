using Microsoft.AspNetCore.Http;
using PetShop.Model.Entities;

namespace PetShop.Services
{
	public class ImageService
	{
		public async Task<string> InsertImage(Animal model, IFormFile formFile)
		{
			var fileName = DateTime.Now.ToString("yyMMddhhmmss") + Path.GetExtension(formFile.FileName);
			if (model.ImageUrl != fileName)
			{
				var filePath = Path.Combine("wwwroot/Images", fileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
					await formFile.CopyToAsync(stream);
			}
			return fileName;

		}
		public void RemoveImage(string fileName)
		{
			var filePath = Path.Combine("wwwroot/Images", fileName);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
	}
}
