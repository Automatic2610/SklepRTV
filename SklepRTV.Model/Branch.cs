using System;

namespace SklepRTV.Model
{
    public class Branch
    {
        public Guid Id { get; set; }
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int HouseNo { get; set; }
        public int? FlatNo { get; set; } 
        public string Province { get; set; } = default!;

     
    }
}
