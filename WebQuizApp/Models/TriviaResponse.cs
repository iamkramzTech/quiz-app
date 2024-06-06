namespace WebQuizApp.Models
{
    public class TriviaResponse
    {
        public int ResponseCode { get; set; }
        public List<QuestionModel> Results { get; set; }
    }
}
