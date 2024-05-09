using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	internal class Customer
	{
		public Guid id { get; private set; } = Guid.NewGuid();
		public Guid userId { get; private set; }
		public virtual ContactDetails contactDetails { get; set; }
		public virtual AddressDetails addressDetails { get; set; }
	}
}
