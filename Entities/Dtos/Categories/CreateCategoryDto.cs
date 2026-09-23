namespace Entities.Dtos.Categories
{
    public class CreateCategoryDto
    {
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}