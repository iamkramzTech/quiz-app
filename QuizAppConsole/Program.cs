using System.Net.Http;
using System.Text.Json;
namespace QuizAppConsole
{
    internal class Program
    {
        private static HttpClient _httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            var triviaSettings = new TriviaSettings
            {
                NumOfQuestion = 5,
                Category = "10",
                Difficulty = "easy",
                Type = "multiple"
            };



            var API = $"https://opentdb.com/api.php?amount={triviaSettings.NumOfQuestion}&category={triviaSettings.Category}&difficulty={triviaSettings.Difficulty}&type={triviaSettings.Type}";

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(API);


                Console.WriteLine(httpResponse);


                // Ensure the request was successful
                httpResponse.EnsureSuccessStatusCode();

                // Read the response content as a string
                var responseBody = await httpResponse.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // Deserialize the JSON response to TriviaResponse object
                var triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(responseBody, options);
                if (triviaResponse == null || triviaResponse.Results == null)
                {
                    Console.WriteLine("No questions found or error in the response.");
                }
                else
                {
                    //triviaQuestion.Category = "9";
                    Console.WriteLine("**** OPENTDB API Response ****");
                    Console.WriteLine("Response Code: {0} Count: {1}",triviaResponse.ResponseCode,triviaResponse.Results.Count);
                    
                    //Console.Write($"Category: {triviaQuestion.Category ??= "Category is null"}\n");
                    //Console.Write($"Type: {triviaQuestion.Type}\n");

                    foreach (var triviaQuestion in triviaResponse.Results)
                    {
                        

                      
                        Console.WriteLine($"Type: {triviaQuestion.Type}");
                        Console.WriteLine($"Difficulty: {triviaQuestion.Difficulty}");
                        Console.WriteLine($"Category: {triviaQuestion.Category}");
                        Console.WriteLine($"Question: {triviaQuestion.Question}");
                        Console.WriteLine("IncorrectAnswers: [");
                        foreach (var item in triviaQuestion.Incorrect_Answers)
                        {
                            Console.Write($"{item +", "}");
                        }
                        Console.Write("]");
                        Console.WriteLine($"\nCorrectAnswer: {triviaQuestion.Correct_Answer}");
                        Console.WriteLine();
                        
                    }
                }
              

            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exceptions
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing exceptions
                Console.WriteLine($"JSON parsing error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("unexpected error: "+ex.Message);
            }

        }

        //static void MethodHelper1(int numOfQuestions, string category, string difficulty, string type)
        //{
        //    var triviaSettings = new TriviaSettings();
        //    triviaSettings.NumOfQuestion = numOfQuestions;
        //    triviaSettings.Category = category;
        //    triviaSettings.Difficulty = difficulty;
        //    triviaSettings.Type = type;

        //}
    }
}




