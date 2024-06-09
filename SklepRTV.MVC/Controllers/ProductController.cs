﻿using Microsoft.AspNet.Identity.EntityFramework;
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

		public IActionResult Details(Guid id)
		{

			var product = _db.Products.FirstOrDefault(p => p.Id == id);

			if (product == null) return NotFound();

			return View(product);
		}

		public IActionResult Create()
		{
			return View();
		}

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
	}
}
