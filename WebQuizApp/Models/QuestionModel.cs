namespace WebQuizApp.Models
{
    public class QuestionModel
    {
        public int NumOfQuestions { get; set; }
        public string? Category { get; set; }
       
        public string? Text { get; set; }

        public string? Type {  get; set; }
        public string? Difficulty { get; set; }
        
        public string? CorrectAnswer { get; set; }

        public List<string>? IncorrectAnswers { get; set; }

    }
}
