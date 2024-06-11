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
            var cartItem = Items.FirstOrDefault(x => x.Id == product.Id);

            if (cartItem != null) cartItem.Quantity += quantity;
            else
                Items.Add(new CartItem { Id = product.Id,Product = product,Quantity = quantity });
        }

        public void RemoveItem(Guid productId)
        {
            var cartItem = Items.FirstOrDefault(x => x.Id == productId); 
            if(cartItem != null) Items.Remove(cartItem);
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var item in Items)
            {
                totalPrice += item.Product.price * item.Quantity;
            }

            return totalPrice;
        }
    }
}
