using SklepRTV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Test
{
    public class ShoppingCart
    {
        [Fact]
        public void AddOneItemToCart()
        {
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            var items = cart.Items;

            Assert.Equal(1, items.Count);

        }
		[Fact]
		public void RemoveOneItemFromCart()
		{
			var cart = new Cart();
			cart.AddItem(new Product(), 8);
			var items = cart.Items.ToList();
			foreach (var item in items)
			{
				cart.RemoveItem(item.Product.Id);
			}
			Assert.Equal(0, cart.Items.Count);
		}
		[Fact]
		public void CalculateItemsFromCart()
		{
			var cart = new Cart();
			cart.AddItem(new Product(), 1);
			var items = cart.Items;
			decimal totalPrice = 0;
			foreach(var item in items)
			{
				totalPrice += item.Product.price;
			}
			decimal total = cart.CalculateTotal();
			Assert.Equal(total, totalPrice);
		}
	}
}
