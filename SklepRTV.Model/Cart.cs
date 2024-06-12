using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var existingtem = Items.Find(item => item.Product.Id == product.Id);
            if (existingtem != null)
            {
                existingtem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void RemoveItem(Guid productId)
        {
            Items.RemoveAll(item => item.Product.Id == productId);
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Product.price * item.Quantity;
            }
            return total;
        }
    }
}
