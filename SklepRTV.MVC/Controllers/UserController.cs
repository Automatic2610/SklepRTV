using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace SklepRTV.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> AddMemberRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            
            if (user == null) return NotFound();
            _userManager.AddToRoleAsync(user, "Member");
            _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddManagerRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();
            _userManager.AddToRoleAsync(user, "Manager");


            return View(user);
        }

        public async Task<IActionResult> AddAdminRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();
            _userManager.AddToRoleAsync(user, "Admin");


            return View(user);
        }

        public async Task<IActionResult> DeactiveUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();
            user.EmailConfirmed = false;

            _userManager.UpdateAsync(user);

            return View(user);
        }

        public async Task<IActionResult> ActivateUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();
            user.EmailConfirmed = true;

            _userManager.UpdateAsync(user);

            return View(user);
        }
    }
}
