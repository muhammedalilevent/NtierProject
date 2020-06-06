using NTier.Core.Entity;
using System;
using System.Collections.Generic;

namespace NTier.Model.Entities
{
    public class SubCategory : CoreEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Tag { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}