namespace Entities.Dtos.Carts
{
    public class CreateCartDto
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}