namespace Entities.Dtos.Orders
{
    public class CreateOrderDto
    {
        public int ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = "Created";
    }
}