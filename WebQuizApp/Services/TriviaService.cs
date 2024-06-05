using Newtonsoft.Json;
using WebQuizApp.Models;

namespace WebQuizApp.Services
{
    public class TriviaService
    {
        private readonly HttpClient _httpClient;

        public TriviaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuestionModel>> GetQuestionsAsync(int numOfQuestions, string category, string difficulty, string type)
        {
            var API = $"https://opentdb.com/api.php?amount={numOfQuestions}&category=${category}&difficulty=${difficulty}&type=${type}";

            var response = await _httpClient.GetStringAsync(API);

            var triviaResponse = JsonConvert.DeserializeObject<TriviaResponse>(response);

            return triviaResponse.Results;
        }
    }
}
