using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{


	public class OrderItemTest
	{
		[Fact]
		public void OrderItemTest_SetProductName()
		{
			var orderItem = new OrderItem();
			var productName = "Suszarka Philips";
			orderItem.ProductName = productName;
			Assert.Equal(productName, orderItem.ProductName);
		}
		[Fact]
		public void OrderItemTest_SetQuantity()
		{
			var orderItem = new OrderItem();
			var quantity = 90;
			orderItem.Quantity = quantity;
			Assert.Equal(quantity, orderItem.Quantity);
		}
		[Fact]
		public void OrderItemTest_SetPrice()
		{
			var orderItem = new OrderItem();
			decimal price = 900;
			orderItem.Price = price;
			Assert.Equal(price, orderItem.Price);
		}
	}
}