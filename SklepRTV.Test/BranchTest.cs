using Microsoft.VisualBasic;
using SklepRTV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Test
{
	public class BranchTest
	{
		[Fact]
		public void Branch_ShouldSetStreet()
		{
			var branch = new Branch();
			var street = "IX Wiekow Kielc";
			branch.Street = street;
			Assert.Equal(street, branch.Street);
		}
		[Fact]
		public void Branch_ShouldSetCity()
		{
			var branch = new Branch();
			var city = "Radom";
			branch.City = city;
			Assert.Equal(city, branch.City);

		}

		[Fact]
		public void Branch_SetHouseNo()
		{
			var branch = new Branch();
			var HouseNo = 20;
			branch.HouseNo = HouseNo;
			Assert.Equal(HouseNo,branch.HouseNo);
		}
		[Fact]
		public void Branch_SetFlatNo()
		{
			var branch = new Branch();
			var flatNo = 20;
			branch.FlatNo = flatNo;
			Assert.Equal(flatNo,branch.FlatNo);
		}
		[Fact]
		public void Branch_SetProvince()
		{
			var branch = new Branch();
			var province = "Mazowieckie";
			branch.Province=province;
			Assert.Equal(province,branch.Province);
		}
	}
}
