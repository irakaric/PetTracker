using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class PravniOblici
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naziv pravnog oblika")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(100, ErrorMessage = "Naziv ne smije biti duži od 100 znakova")]
        public required string Naziv { get; set; }

        public bool Status { get; set; } = true;
    }
}
