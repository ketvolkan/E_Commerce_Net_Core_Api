using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = "Created";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public Address? ShippingAddress { get; set; }
        public Address? BillingAddress { get; set; }
        public ICollection<SubOrder> SubOrders { get; set; } = new List<SubOrder>();
    }
}
