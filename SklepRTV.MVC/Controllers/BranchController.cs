using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SklepRTV.MVC.Data;
using SklepRTV.Model;
using System;
using System.Threading.Tasks;

namespace SklepRTV.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BranchController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /Branch/Index
        public async Task<IActionResult> Index()
        {
            var branches = await _db.Branches.ToListAsync();
            return View(branches);
        }

        // GET: /Branch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Branch/Create
        [HttpPost]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _db.Branches.Add(branch);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: /Branch/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: /Branch/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(branch);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: /Branch/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: /Branch/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _db.Branches.Remove(branch);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Branch/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var branch = await _db.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        private bool BranchExists(Guid id)
        {
            return _db.Branches.Any(e => e.Id == id);
        }
    }
}
