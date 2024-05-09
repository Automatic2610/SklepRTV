using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class Branch
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		string city { get; set; } = default!;
		string street { get; set; } = default!;
		int houseNo { get; set; }
		int flatNo { get; set; }
		int countryId { get; set; }
		string province { get; set; } = default!;
	}
}

