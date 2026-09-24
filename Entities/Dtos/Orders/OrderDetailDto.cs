using System;
using System.Collections.Generic;

namespace Entities.Dtos.Orders
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string ShippingAddressTitle { get; set; } = string.Empty;
        public string ShippingAddressCity { get; set; } = string.Empty;
        public string ShippingAddressDistrict { get; set; } = string.Empty;
        public string ShippingAddressDetail { get; set; } = string.Empty;
        public List<SubOrderDetailDto> SubOrders { get; set; } = new();
    }

    public class SubOrderDetailDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string SubOrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CargoTrackingNumber { get; set; } = string.Empty;
        public string CargoCompany { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public List<OrderItemDetailDto> Items { get; set; } = new();
    }

    public class OrderItemDetailDto
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string VariantInfo { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
