namespace Entities.Dtos.Orders
{
    public class CheckoutDto
    {
        public int ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
    }
}
