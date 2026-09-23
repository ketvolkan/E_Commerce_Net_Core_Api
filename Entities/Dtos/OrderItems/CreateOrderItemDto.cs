namespace Entities.Dtos.OrderItems
{
    public class CreateOrderItemDto
    {
        public int SubOrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CommissionRate { get; set; }
    }
}