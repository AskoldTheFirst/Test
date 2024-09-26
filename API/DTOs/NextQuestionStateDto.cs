namespace API.DTOs
{
    public class NextQuestionStateDto
    {
        public QuestionDto Question { get; set; }

        public int QuestionNumber { get; set; }

        public int TotalAmount { get; set; }

        public int SecondsLeft { get; set; }

        public string TechnologyName { get; set; }
    }
}