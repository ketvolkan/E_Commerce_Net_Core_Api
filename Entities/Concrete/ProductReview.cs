using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
namespace Entities.Concrete
{
    public class ProductReview : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
        public User? User { get; set; }
        public Store? Store { get; set; }
    }
}
