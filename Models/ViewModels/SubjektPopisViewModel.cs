namespace ZavrsniRad.Models.ViewModels
{
    public class SubjektPopisViewModel
    {
        public required Subjekti Subjekt { get; set; }

        public string Role { get; set; } = string.Empty;

        public bool JePrijavljeniAdministrator { get; set; }
    }
}
