using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;
namespace SklepRTV.Test
{
	public class CountryTest
	{
		[Fact]
		public void CountryTest_SetIdCountry()
		{
			var country = new Country();
			var idC = 5;
			country.idCountry = idC;
			Assert.Equal(idC, country.idCountry);
		}
		[Fact]
		public void CountryTest_SetCountry()
		{
			var country = new Country();
			var nameC = "Polska";
			country.country = nameC;
			Assert.Equal(nameC, country.country);
		}
	}
}
