using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;
namespace SklepRTV.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IApplicationDbContext _context;
        public CategoryController(IApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Details(int id)
        {
            var category = _context.Categories.FirstOrDefault(p => p.Id ==
           id);
            if (category == null) return NotFound();
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id ==
           id);
            if (category == null) return NotFound();
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id ==
           id);
            if (category == null) return NotFound();
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id ==
           id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("AdminIndex");
        }
    }
}
