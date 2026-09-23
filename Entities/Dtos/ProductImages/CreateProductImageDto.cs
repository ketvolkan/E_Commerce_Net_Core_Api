namespace Entities.Dtos.ProductImages
{
    public class CreateProductImageDto
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}