using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class UsertypeTest
	{
		[Fact]
		public void UserType_setType()
		{
			var userType = new UserType();
			var id = 1;
			userType.id = id;
			Assert.Equal(id, userType.id);
		}
		[Fact]
		public void UserType_SetName()
		{
			var userType = new UserType();
			var name = "Gabrysia";
			userType.Name = name;
			Assert.Equal(name, userType.Name);
		}
	}
}
