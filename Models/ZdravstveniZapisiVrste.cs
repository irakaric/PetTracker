using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class ZdravstveniZapisiVrste
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Vrsta zdravstvenog zapisa")]
        [Required(ErrorMessage = "Naziv vrste zdravstvenog zapisa je obavezan")]
        [StringLength(100,ErrorMessage = "Naziv ne smije biti duži od 100 znakova")]
        public required string Naziv { get; set; }

        public bool Status { get; set; } = true;
    }
}
