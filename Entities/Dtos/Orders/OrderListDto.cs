namespace Entities.Dtos.Orders
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}