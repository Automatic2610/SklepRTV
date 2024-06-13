using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class AddressDetails
	{
		public string city { get; set; } = default!;
		public string street { get; set; } = default!;
		public int houseNo { get; set; }
		public int flatNo { get; set; }
		public string province { get; set; } = default!;
	}
}
