namespace Entities.Dtos.SubOrders
{
    public class UpdateSubOrderDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public string SubOrderNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CargoTrackingNumber { get; set; } = string.Empty;
        public string CargoCompany { get; set; } = string.Empty;
    }
}