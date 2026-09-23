namespace Entities.Dtos.Products
{
    public class CreateProductImageDto
    {
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
