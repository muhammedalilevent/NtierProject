using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTierProje.UI.Areas.Admin.Models
{
    public class ProductUpdateVM
    {
        public List<SubCategory> SucCategories { get; set; }
        public Product Product { get; set; }
    }
}