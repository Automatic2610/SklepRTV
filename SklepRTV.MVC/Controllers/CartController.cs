using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;
using System.Text.Json;



namespace SklepRTV.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

       

        public IActionResult Index()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            Cart cart;

            if(cartJson != null) cart = JsonSerializer.Deserialize<Cart>(cartJson);
            else cart = new Cart();

            return View(cart);
        }

        public IActionResult AddToCart(Guid id, int quantity)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var cartJson = HttpContext.Session.GetString("Cart");
                Cart cart;
                if (cartJson != null) cart = JsonSerializer.Deserialize<Cart>(cartJson);
                else cart = new Cart();

                cart.AddItem(product, quantity);

                cartJson = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString("Cart", cartJson);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            Cart cart;
            if (cartJson != null) cart = JsonSerializer.Deserialize<Cart>(cartJson);
            else cart = new Cart();

            cart.RemoveItem(id);

            cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);


            return RedirectToAction("Index");
        }
    }
}
