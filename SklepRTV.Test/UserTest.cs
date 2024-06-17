using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;
namespace SklepRTV.Test
{
	public class UserTest
	{
		[Fact]
		public void UserTest_SetType()
		{
			var userTest = new User();
			var type = 2;
			userTest.Type = type;
			Assert.Equal(type, userTest.Type);
		}
		[Fact]
		public void UserTest_SetlastName() 
		{
			var userTest = new User();
			var lastName = "Wisniewski";
			userTest.lastName = lastName;
			Assert.Equal(lastName, userTest.lastName);
		}
		[Fact]
		public void UserTest_SetEmail() 
		{
			var userTest = new User();
			var email = "mateusz@wisniewski.klc";
			userTest.email = email;
			Assert.Equal (email, userTest.email);

		}
		[Fact]
		public void UserTest_SetPhone() 
		{
			var userTest = new User();
			var phone = "987654321";
			userTest.phone = phone;
			Assert.Equal(phone, userTest.phone);
		}
		[Fact]
		public void UserTest_SetPassword() 
		{
			var userTest = new User();
			var password = "Ip@123";
			userTest.password = password;
			Assert.Equal(password, userTest.password);
		}
		[Fact]
		public void User_SetUsername()
		{
			var userTest = new User();
			var username = "mateusz_wisniewski";
			userTest.username = username;
			Assert.Equal(username, userTest.username);
		}
	}
}
