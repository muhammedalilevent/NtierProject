using NTier.Core.Entity;

namespace NTier.Model.Entities
{
    public class OrderDetails : CoreEntity
    {
        public virtual Product Product { get; set; }
        public virtual Orders Orders { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
    }
}