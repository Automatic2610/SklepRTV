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
		public String lastName { get; set; } = default!;
		public String email { get; set; } = default!;
		public String phone { get; set; } = default!;
		public String username { get; set; } = default!;
		public String password { get; set; } = default!;
	}
}
