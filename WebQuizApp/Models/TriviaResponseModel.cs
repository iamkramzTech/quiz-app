namespace WebQuizApp.Models
{
    public class TriviaResponseModel
    {
        public int ResponseCode { get; set; }
        public List<TriviaQuestionModel> Results { get; set; }
    }
}
