namespace Entities.Dtos.CartItems
{
    public class UpdateCartItemDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}