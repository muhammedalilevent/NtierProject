using NTier.Core.Entity;
using System.Collections.Generic;

namespace NTier.Model.Entities
{
    public class Category : CoreEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}