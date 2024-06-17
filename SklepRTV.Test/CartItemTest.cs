using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class CartItemTest
	{
		[Fact]
		public  void CartItem_SetQuantity() 
		{
			var cartItem = new CartItem();
			var quantity = 5;
			cartItem.Quantity = quantity;
			Assert.Equal(quantity, cartItem.Quantity);
		}
	}
}
