using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;

namespace SklepRTV.MVC.Controllers
{
	public class ProductController : Controller
	{

		private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
		{
			var products = _db.Products.ToList();
			


			return View(products);
		}

        [Authorize(Roles = "Admin")]
		[HttpGet]
        public IActionResult AdminIndex()
		{
            var products = _db.Products.ToList();

            return View(products);
        }

		public IActionResult Details(Guid id)
		{

			var product = _db.Products.FirstOrDefault(p => p.Id == id);

			if (product == null) return NotFound();

			return View(product);
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public IActionResult Create(Product product)
		{
			if(ModelState.IsValid)
			{
				_db.Products.Add(product);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(product);
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(Guid id)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);

			if(product == null) return NotFound();

			return View(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if(ModelState.IsValid)
			{
				_db.Products.Update(product);
				_db.SaveChanges();

				return RedirectToAction("AdminIndex");
			}

			return View(product);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult Delete(Guid id)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);
			if(product == null) return NotFound();

			return View(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(Guid id)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);

			if(product == null) return NotFound();
			_db.Products.Remove(product);
			_db.SaveChanges();

			return RedirectToAction("AdminIndex");
		}
	}
}
