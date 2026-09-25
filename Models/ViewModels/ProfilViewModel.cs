namespace ZavrsniRad.Models.ViewModels
{
    public class ProfilViewModel
    {
        public int RasporedId { get; set; }

        public int SubjektId { get; set; }

        public bool Aktivan { get; set; }

        public bool JePoslovni { get; set; }

        public string Naziv { get; set; } = string.Empty;
    }
}
