using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }
        public int SubOrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CommissionRate { get; set; }

        public SubOrder? SubOrder { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
