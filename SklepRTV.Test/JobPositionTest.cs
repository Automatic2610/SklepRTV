using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SklepRTV.Model;

namespace SklepRTV.Test
{
	public class JobPositionTest
	{
		[Fact]
		public void JobPositionTest_SetId()
		{
			var jobPosition = new JobPosition();
			var id = 5;
			jobPosition.id = id;
			Assert.Equal(id, jobPosition.id);
		}
		[Fact]
		public void JobPosiotionTest_SetName()
		{
			var jobPosition = new JobPosition();
			var name = "Szymon";
			jobPosition.name = name;
			Assert.Equal(name, jobPosition.name);
		}
	}
}
