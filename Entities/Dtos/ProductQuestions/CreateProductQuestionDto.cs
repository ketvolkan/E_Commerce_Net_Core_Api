namespace Entities.Dtos.ProductQuestions
{
    public class CreateProductQuestionDto
    {
        public int ProductId { get; set; }
        // UserId will be assigned from the authenticated user on the server
        public int StoreId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
    }
}
