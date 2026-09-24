using System.Collections.Generic;
using Entities.Dtos.ProductImages;
using Entities.Dtos.ProductVariants;
namespace Entities.Dtos.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<UpdateProductImageDto> Images { get; set; } = new();
        public List<UpdateProductVariantDto> Variants { get; set; } = new();
    }
}
