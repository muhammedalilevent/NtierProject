﻿using NTier.Core.Map;
using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Map
{
    public class CategoryMap : CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.Name).IsRequired();
            Property(x => x.Description).IsOptional();

            HasMany(x => x.SubCategories)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryID);
        }
    }
}
