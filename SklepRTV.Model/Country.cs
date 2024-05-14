using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
	public class Country
	{
		[Key]
		public int idCountry  { get; set; }
		public string country { get; set; } = default!;
	}
}
