using Microsoft.AspNetCore.Mvc;
using WebQuizApp.Models;
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
            var correctAnswer = questions.Select(q => q.Correct_Answer).ToList();
            TempData["Correct_Answers"] = correctAnswer;
            TempData.Keep("Correct_Answers");
            ViewBag.Countdown = settings.Countdown;
            return View("StartQuiz", questions);
        }

        //[HttpPost]
        //public IActionResult Result(List<string> userAnswers)
        //{
        //    var correct_Answer = TempData["Correct_Answer"] as List<string>;
        //    int score = 0;
        //    for (int i = 0; i < userAnswers.Count; i++)
        //    {
        //        if (userAnswers[i] == correct_Answer[i])
        //        {
        //            score++;
        //        }
        //    }
        //    ViewBag.Score = score;
        //    return View("Result", userAnswers);
        //}

        [HttpPost]
        public IActionResult Result(List<string> userAnswers)
        {
            // Retrieve the correct answers from TempData
            var correct_Answer = TempData.Peek("Correct_Answers") as List<string>;

            // Initialize the score counter
            var score = 0;

            // Ensure that both userAnswers and correct_Answers are not null
            if (userAnswers != null && correct_Answer != null)
            {
                for (int i = 0; i < userAnswers.Count; i++)
                {
                    // Safeguard against mismatched lengths
                    if (i < correct_Answer.Count && userAnswers[i] == correct_Answer[i])
                    {
                        score++;
                    }
                }
            }
            else
            {
                // Handle the case where correct_Answers is null
                ModelState.AddModelError(string.Empty, "Correct answers are not available.");
            }

            // Store the score in ViewBag for the view to display
            ViewBag.Score = score;

            // Return the Result view, passing userAnswers as the model
            return View("Result", userAnswers);
        }

    }
}
