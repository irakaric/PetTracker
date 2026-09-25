using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class ZivotinjeStatusi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv ne smije biti duže od 50 znakova")]
        public required string Naziv { get; set; }

        public bool Status { get; set; } = true;
    }
}
