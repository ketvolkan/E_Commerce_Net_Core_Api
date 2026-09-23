namespace Entities.Dtos.Categories
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}