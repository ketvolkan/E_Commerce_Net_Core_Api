namespace Entities.Dtos.ProductQuestions
{
    public class UpdateProductQuestionDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string AnswerText { get; set; } = string.Empty;
    }
}
