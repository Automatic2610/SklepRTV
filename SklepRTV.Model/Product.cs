﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepRTV.Model
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        public string name { get; set; } = default!;
        public decimal price { get; set; }
        public int unitId { get; set; }
        public int quantity { get; set; }
        public string description { get; set; } = default!;
        public int stock { get; set; }
        public virtual Warehouse[] warehouses { get; set; } = { };
        public virtual Category[] Categories { get; set; } = { };
        public string? image { get; set; } = default!;
    }
}
