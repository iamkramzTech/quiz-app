namespace WebQuizApp.Models
{
    public class TriviaSettingsModel
    {
        public int NumOfQuestion { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public string Type { get; set; }
        public int Countdown { get; set; }
    }
}
