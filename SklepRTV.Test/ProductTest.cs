using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class ProductTest
	{
		[Fact]
		public void Product_SetName()
		{
			var product = new Product();
			var name = "Mati";
			product.name= name;
			Assert.Equal(name, product.name);
		}
		[Fact]
		public void Product_SetPrice()
		{
			var product = new Product();
			decimal price = 10;
			product.price = price;
			Assert.Equal(price, product.price);
		}
		[Fact]
		public void Product_SetDescription()
		{
			var product = new Product();
			var description = "Masniutko, łatwo ni";
			product.description = description;
			Assert.Equal(description, product.description);
		}
		[Fact]
		public void Product_SetProductUnitId() { 
			var product = new Product();
			var unitId = 15;
			product.unitId = unitId;
			Assert.Equal(unitId, product.unitId);
		}
		[Fact]
		public void Product_SetProductQuantity() 
		{
			var product = new Product();
			var Quantity = 5;
			product.quantity=Quantity;
			Assert.Equal(Quantity, product.quantity);
		}
		[Fact]
		public void Product_SetProductStock()
		{
			var product = new Product();
			var stock = 25;
			product.stock = stock;
			Assert.Equal(stock, product.stock);

		}
		[Fact]
		public void Product_SetImage()
		{
			var product = new Product();
			var image = "SrajacyKot";
			product.image = image;
			Assert.Equal(image, product.image);
		}
	}
}
