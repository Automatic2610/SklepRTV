using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class Worker
	{
		public Guid id { get; private set; } = Guid.NewGuid();
		public Guid userId { get; private set; }
        public ContactDetails contactDetails { get; set; } = default!;
        public AddressDetails addressDetails { get; set; } = default!;
    }
}
