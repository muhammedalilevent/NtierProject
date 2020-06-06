using NTier.Core.Entity;
using System;
using System.Collections.Generic;

namespace NTier.Model.Entities
{
    public class Orders : CoreEntity
    {
        public Orders()
        {
            OrderDetails = new List<Entities.OrderDetails>();
        }
        public Guid AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }
        public bool Confirmed { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}