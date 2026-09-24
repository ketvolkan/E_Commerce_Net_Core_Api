using System.Collections.Generic;

namespace Entities.Dtos.Carts
{
    public class CartDetailDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public List<CartItemDetailDto> Items { get; set; } = new();
    }

    public class CartItemDetailDto
    {
        public int CartItemId { get; set; }
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal => Price * Quantity;
    }

    public class AddToCartDto
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
