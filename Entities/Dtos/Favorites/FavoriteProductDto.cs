namespace Entities.Dtos.Favorites
{
    public class FavoriteProductDto
    {
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal MinPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
