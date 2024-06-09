using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class User
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public int Type { get; set; }
		public string lastName { get; set; } = default!;
		public string email { get; set; } = default!;
		public string phone { get; set; } = default!;
		public string username { get; set; } = default!;
		public string password { get; set; } = default!;
	}
}
