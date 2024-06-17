using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class ContactDetailsTest
	{
		[Fact]
		public void ContactDetails_SetEmail()
		{
			var contactDetails = new ContactDetails();
			var email = "mateusz@example.ex";
			contactDetails.email = email;
			Assert.Equal(email,contactDetails.email);
		}
		[Fact]
		public void ContactDetails_SetPhone()
		{
			var contactDetails = new ContactDetails();
			var phone = "123456789";
			contactDetails.phone = phone;
			Assert.Equal(phone,contactDetails.phone);
		}

	}
}
