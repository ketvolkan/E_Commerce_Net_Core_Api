namespace Entities.Dtos.SubOrders
{
    public class CreateSubOrderDto
    {
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public string SubOrderNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Preparing";
        public string CargoTrackingNumber { get; set; } = string.Empty;
        public string CargoCompany { get; set; } = string.Empty;
    }
}