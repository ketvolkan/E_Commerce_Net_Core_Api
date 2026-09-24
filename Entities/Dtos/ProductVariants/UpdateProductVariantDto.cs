namespace Entities.Dtos.ProductVariants
{
    public class UpdateProductVariantDto
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
        public bool IsActive { get; set; } = true;
    }
}