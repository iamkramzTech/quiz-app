using Microsoft.AspNetCore.Mvc;
using WebQuizApp.Models;
using WebQuizApp.ViewModels;
using WebQuizApp.Services;

namespace WebQuizApp.Controllers
{
    public class TriviaQuizController : Controller
    {
        private readonly TriviaService _triviaService;

        public TriviaQuizController(TriviaService triviaService)
        {
            _triviaService = triviaService;
        }

        [HttpGet]
        public IActionResult QuizSettings()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StartQuiz(TriviaSettingsModel settings)
        {
            var questions = await _triviaService.GetQuestionsAsync(settings.NumOfQuestion, settings.Category, settings.Difficulty, settings.Type);

            var model = new QuizViewModel
            {
                TriviaQuestions = questions
            };
            ViewBag.Countdown = settings.Countdown;
            return View("StartQuiz", model);
        }
        
        [HttpPost]
        public IActionResult Result(QuizViewModel model)
        {
            // Debugging: Log model data
           // Console.WriteLine("Questions: " + (model.TriviaQuestions != null ? string.Join(", ", model.TriviaQuestions.Select(q => q.Question)) : "null"));
            //Console.WriteLine("UserAnswers: " + (model.UserAnswers != null ? string.Join(", ", model.UserAnswers) : "null"));
            // Initialize UserAnswers if it is null
            model.UserAnswers ??= new List<string>();

            // Initialize Questions if it is null
            model.TriviaQuestions ??= new List<TriviaQuestionModel>();

            for (var i = 0; i < model.UserAnswers.Count; i++)
            {
                if (i< model.TriviaQuestions.Count && model.UserAnswers[i] == model.TriviaQuestions[i].Correct_Answer)
                {
                    model.Score++;
                }
            }
            return View("Result", model);
        }

    }

}
