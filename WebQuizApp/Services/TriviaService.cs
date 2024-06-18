using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;
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

        public async Task<List<TriviaQuestionModel>> GetQuestionsAsync(int numOfQuestions, string category, string difficulty, string type)
        {
            var API = $"https://opentdb.com/api.php?amount={numOfQuestions}&category={category}&difficulty={difficulty}&type={type}";

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(API);
            Console.WriteLine(httpResponseMessage);

            // Ensure the request was successful
            httpResponseMessage.EnsureSuccessStatusCode();

            // Read the response content as a string
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            var triviaResponse = JsonConvert.DeserializeObject<TriviaResponseModel>(responseBody);

            Console.WriteLine($"Response Code: {triviaResponse.ResponseCode} Count: {triviaResponse.Results.Count}");
            Console.WriteLine(triviaResponse.Results);
            if (triviaResponse?.Results == null)
            {
                return new List<TriviaQuestionModel>();
            }
            // Ensure each question has its IncorrectAnswers list initialized
            foreach (var triviaQuestion in triviaResponse.Results)
            {
                if (triviaQuestion.Incorrect_Answers == null)
                {
                    triviaQuestion.Incorrect_Answers = new List<string>();
                }
            }
            //Console.WriteLine(triviaResponse.Results);
            return triviaResponse.Results;
        }
    }
}
