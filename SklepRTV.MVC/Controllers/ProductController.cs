using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;
using System.Diagnostics;

namespace SklepRTV.MVC.Controllers
{
	public class ProductController : Controller
	{

		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _environment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
			_environment = environment;
        }

        public IActionResult Index()
		{
			var products = _db.Products.ToList();
			


			return View(products);
		}

        [Authorize(Roles = "Admin,Manager")]
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
		public async Task<IActionResult> Create(Product product, IFormFile image)
		{

			if (image == null)
			{
				Debug.WriteLine("Brak pliku");
				return Content("Brak pliku");
			}
			if(image.Length == 0)
			{
				Debug.WriteLine("Błędny plik");
				return Content("Błędny plik");
			}
					
						var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", image.FileName);
						

						using(var stream = new FileStream(path,FileMode.Create))
				{
					await image.CopyToAsync(stream);
				}

				product.image = path;
					
			if(ModelState.IsValid) 
			{
					_db.Products.Add(product);
					_db.SaveChanges();
					return RedirectToAction("Index");
			
			}
			else
			{
				var errors = ModelState.Values.SelectMany(x => x.Errors);
				foreach(var error in errors)
				{
					Debug.WriteLine($"Błąd modelu: {error.ErrorMessage}");
				}
                ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku");
            }

            return View(product);
		}
		[Authorize(Roles = "Admin,Manager")]
		public IActionResult Edit(Guid id)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);

			if(product == null) return NotFound();

			return View(product);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Edit(Product product, IFormFile image)
		{
			if(ModelState.IsValid)
			{
				try
				{
					if (image != null)
					{
						var uploads = Path.Combine(_environment.WebRootPath, "uploads");
						if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

						var filePath = Path.Combine(uploads, image.FileName);
						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							image.CopyToAsync(fileStream);
						}

						product.image = $"/uploads/{image.FileName}";
					}
                    _db.Products.Update(product);
                    _db.SaveChangesAsync();
                }
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, "Błąd w zapisie pliku");
                    return View(product);
                }

              

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
