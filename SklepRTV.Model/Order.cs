using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class Order
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public Product[] products { get; set; }
		public double total { get; set; }
		public Guid customerId { get; private set; }
	}
}
