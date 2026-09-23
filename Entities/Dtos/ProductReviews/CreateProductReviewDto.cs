namespace Entities.Dtos.ProductReviews
{
    public class CreateProductReviewDto
    {
        public int ProductId { get; set; }
        // UserId will be set from authenticated user
        public int StoreId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
