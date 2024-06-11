using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
