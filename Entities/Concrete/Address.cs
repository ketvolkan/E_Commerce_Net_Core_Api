using Core.Entities;
using System;
using Core.Entities.Concrete;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string AddressDetail { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
