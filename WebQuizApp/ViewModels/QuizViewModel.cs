using WebQuizApp.Models;
namespace WebQuizApp.ViewModels
{
    public class QuizViewModel
    {
        public List<TriviaQuestionModel>? TriviaQuestions { get; set; }
        public List<string> UserAnswers { get; set; } = new List<string>();
        public int Score { get; set; }
    }
}
