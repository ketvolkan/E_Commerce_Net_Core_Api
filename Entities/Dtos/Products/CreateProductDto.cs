using System.Collections.Generic;

namespace Entities.Dtos.Products
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<CreateProductImageDto> Images { get; set; } = new();
        public List<CreateProductVariantDto> Variants { get; set; } = new();
    }
}
