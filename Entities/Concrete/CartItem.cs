using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }

        public Cart? Cart { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
