namespace Entities.Dtos.Products
{
    public class UpdateProductImageDto
    {
        public int? Id { get; set; } // Null ise yeni görsel ekleniyor, dolu ise var olan güncelleniyor
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
