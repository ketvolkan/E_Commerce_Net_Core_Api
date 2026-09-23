using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class ProductQuestion : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string AnswerText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AnsweredAt { get; set; }

        public Product? Product { get; set; }
        public User? User { get; set; }
        public Store? Store { get; set; }
    }
}
