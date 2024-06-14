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
    }
}
