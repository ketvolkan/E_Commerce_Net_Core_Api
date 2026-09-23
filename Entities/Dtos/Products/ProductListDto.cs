namespace Entities.Dtos.Products
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public decimal MinPrice { get; set; }
        public string MainImageUrl { get; set; } = string.Empty;
    }
}
