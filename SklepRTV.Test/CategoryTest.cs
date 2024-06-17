using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;
namespace SklepRTV.Test
{
	public class CategoryTest
	{
		/* ustawianie jest ustawione na prywatne
		[Fact]
		public void CategoryTest_SetId()
		{
			var categoryTest= new Category();
			var id = 5;
			categoryTest.Id = id;
			Assert.Equal(id, categoryTest.Id);
		}
		*/
		[Fact]
		public void CategoryTest_SetName()
		{
			var categoryTest = new Category();
			var name = "Mateusz";
			categoryTest.Name = name;
			Assert.Equal(name, categoryTest.Name);
		}
	}
}
