namespace Entities.Dtos.Products
{
    public class ProductFilterDto
    {
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? StoreId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; } // "price_asc", "price_desc", "newest", "name"
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
