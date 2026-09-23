namespace Entities.Dtos.Stores
{
    public class StoreListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}