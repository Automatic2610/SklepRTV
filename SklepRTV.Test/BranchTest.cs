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
			branch.street = street;
			Assert.Equal(street, branch.street);
		}
		[Fact]
		public void Branch_ShouldSetCity()
		{
			var branch = new Branch();
			var city = "Radom";
			branch.city = city;
			Assert.Equal(city, branch.city);

		}
		[Fact]
		public void Branch_ShouldSetCountry()
		{
			var branch = new Branch();
			var country = 1;
			branch.countryId=country;
			Assert.Equal(country, branch.countryId);

		}
		[Fact]
		public void Branch_SetHouseNo()
		{
			var branch = new Branch();
			var HouseNo = 20;
			branch.houseNo = HouseNo;
			Assert.Equal(HouseNo,branch.houseNo);
		}
		[Fact]
		public void Branch_SetFlatNo()
		{
			var branch = new Branch();
			var flatNo = 20;
			branch.flatNo = flatNo;
			Assert.Equal(flatNo,branch.flatNo);
		}
		[Fact]
		public void Branch_SetProvince()
		{
			var branch = new Branch();
			var province = "Mazowieckie";
			branch.province=province;
			Assert.Equal(province,branch.province);
		}
	}
}
