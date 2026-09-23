namespace Entities.Dtos.Favorites
{
    public class UpdateFavoriteDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
