using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class Djelatnosti
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Oznaka je obavezno polje")]
        [StringLength(10, ErrorMessage = "Oznaka ne smije biti duža od 10 znakova")]
        public required string Oznaka { get; set; }

        [Display(Name = "Naziv djelatnosti")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv ne smije biti duži od 50 znakova")]
        public required string Naziv { get; set; }

        public bool Status { get; set; } = true;
    }
}
