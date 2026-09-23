using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductVariant : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;

        public Product? Product { get; set; }
        public Store? Store { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
