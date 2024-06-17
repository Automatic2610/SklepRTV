using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class AddressDetailsTest
	{ 
		[Fact]
		public void AddressDetails_SetCity()
		{
			var addressdetails = new AddressDetails();
			var city = "Kielce";
			addressdetails.city= city;
			Assert.Equal(city, addressdetails.city);
		}
		[Fact]
		public void AddressDetails_SetStreet()
		{
			var addressdetails = new AddressDetails();
			var street = "Warszawska";
			addressdetails.street= street;
			Assert.Equal(street, addressdetails.street);
		}
		[Fact]
		public void AddressDetails_SetHouseNo()
		{
			var addressdetails = new AddressDetails();
			var houseNo = 53;
			addressdetails.houseNo= houseNo;
			Assert.Equal(houseNo, addressdetails.houseNo);
		}
		[Fact]
		public void AddressDetails_SetFlatNo()
		{
			var addressdetails = new AddressDetails();
			var flatNo = 7;
			addressdetails.flatNo= flatNo;
			Assert.Equal(flatNo, addressdetails.flatNo);
		}
		[Fact]
		public void AddressDetails_SetProvince() 
		{ 
			var addressdetails = new AddressDetails();
			var province = "Swietokrzyskie";
			addressdetails.province= province;
			Assert.Equal(province, addressdetails.province);
		}

	}
}
