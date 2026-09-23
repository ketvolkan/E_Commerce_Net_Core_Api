using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public ICollection<ProductQuestion> ProductQuestions { get; set; } = new List<ProductQuestion>();
    }
}
