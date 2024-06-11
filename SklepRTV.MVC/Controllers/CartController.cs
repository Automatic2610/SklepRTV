using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;
using SklepRTV.Infrastructure;


namespace SklepRTV.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(Guid id, int quantity)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                var cart = GetCart();
                cart.AddItem(product, quantity);
                SaveCart(cart);
            }
            return RedirectToAction("Index");

        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var cart = GetCart();
            cart.RemoveItem(id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }
    }
}
