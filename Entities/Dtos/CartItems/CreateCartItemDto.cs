namespace Entities.Dtos.CartItems
{
    public class CreateCartItemDto
    {
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}