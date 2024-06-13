using Microsoft.AspNetCore.Mvc;
using SklepRTV.Model;
using SklepRTV.MVC.Data;
using System.Text.Json;

namespace SklepRTV.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Checkout()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            Cart cart = cartJson != null ? JsonSerializer.Deserialize<Cart>(cartJson) : new Cart();

            if (!cart.Items.Any())
            {
                return RedirectToAction("PlaceOrder");
            }


            return View(cart);
        }

        [HttpPost]
        public ActionResult PlaceOrder(string customerName, string customerEmail, AddressDetails customerAddress)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            Cart cart = cartJson != null ? JsonSerializer.Deserialize<Cart>(cartJson) : new Cart();

            if (cart.Items.Any())
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    CustomerName = customerName,
                    CustomerEmail = customerEmail,
                    CustomerAddress = customerAddress,
                    TotalAmount = cart.CalculateTotal()
                };

                foreach (var item in cart.Items)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = item.Product.Id,
                        ProductName = item.Product.name,
                        Quantity = item.Quantity,
                        Price = item.Product.price
                    });
                }

                _db.Orders.Add(order);
                _db.SaveChanges();

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("OrderConfirmation", new { order = order.Id });
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult OrderConfirmation(Guid orderId)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return RedirectToAction("OrderSubmitingEnd");
            }

            return View(order);
        }

        public IActionResult OrderSubmitingEnd()
        {
            return View();
        }
    }
}
