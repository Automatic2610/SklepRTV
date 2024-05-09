using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
    public class Warehouse
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string name { get; set; } = default;
        public Guid branchId { get; private set;}
    }
}











