using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;

namespace SklepRTV.MVC.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			var products = new List<SklepRTV.Model.Product>();
			products.Add(new SklepRTV.Model.Product
			{
				name = "Mateusz",
				price = 25.624f,
				unitId = 4,
				quantity = 43,
				description = "uga buga",
				stock = 5

			});
			return View(products);
		}
		[Route("Delete/{Id}")]
		public IActionResult Delete(Guid Id)
		{
			
			return View(Id); 
		}
	}
}
