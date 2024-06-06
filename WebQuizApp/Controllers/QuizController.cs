using Microsoft.AspNetCore.Mvc;
using WebQuizApp.Models;
using WebQuizApp.Services;

namespace WebQuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly TriviaService _triviaService;
        public QuizController(TriviaService triviaService)
        {
            _triviaService = triviaService;
        }
        [HttpGet]
        public IActionResult QuizSettings()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StartQuiz(QuestionModel settings)
        {
            var questions = await _triviaService.GetQuestionsAsync(settings.NumOfQuestions, settings.Category, settings.Difficulty, settings.Type);
            TempData["CorrectAnswers"] = questions.Select(q => q.CorrectAnswer).ToList();
            ViewBag.Countdown = settings.Countdown;
            return View("StartQuiz", questions);
        }

        [HttpPost]
        public IActionResult Result(List<string> userAnswers)
        {
            var correctAnswers = TempData["CorrectAnswers"] as List<string>;
            int score = 0;
            for (int i = 0; i < userAnswers.Count; i++)
            {
                if (userAnswers[i] == correctAnswers[i])
                {
                    score++;
                }
            }
            ViewBag.Score = score;
            return View("Result", userAnswers);
        }
    }
}
