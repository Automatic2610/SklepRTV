using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class Order
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		public string CustomerName { get; set; } = default!;
		public string CustomerEmail { get; set; } = default!;
		public AddressDetails CustomerAddress { get; set; } = default!;
		public decimal TotalAmount { get; set; }
		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}
}
