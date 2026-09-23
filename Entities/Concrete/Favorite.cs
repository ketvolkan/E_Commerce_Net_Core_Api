using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
