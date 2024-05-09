using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string name { get; set; } = default!;
        public double price { get; set; }
        public int unitId { get; set; }
        public int quantity { get; set; }
        public string description { get; set; } = default!;
        public int stock { get; set; }
        public Warehouse[] warehouses { get; set; }
    }
}
