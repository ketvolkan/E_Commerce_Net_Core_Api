using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SubOrder : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public string SubOrderNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CargoTrackingNumber { get; set; } = string.Empty;
        public string CargoCompany { get; set; } = string.Empty;

        public Order? Order { get; set; }
        public Store? Store { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
