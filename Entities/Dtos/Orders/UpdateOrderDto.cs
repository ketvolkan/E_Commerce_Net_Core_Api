namespace Entities.Dtos.Orders
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}