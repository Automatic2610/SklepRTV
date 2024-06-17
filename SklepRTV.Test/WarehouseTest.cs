using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class WarehouseTest
	{
		[Fact]
		public void Warehouse_SetName()
		{
			var warehouse = new Warehouse();
			var name = "Kacper";
			warehouse.name = name;
			Assert.Equal(name, warehouse.name);

		}
	}
}
