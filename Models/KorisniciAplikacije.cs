using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class KorisniciAplikacije
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "Korisničko ime je obavezno polje")]
        [StringLength(50, ErrorMessage = "Korisničko ime ne smije biti duže od 50 znakova")]
        public required string Username { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [StringLength(50, ErrorMessage = "Lozinka ne smije biti duža od 50 znakova")]
        public required string Password { get; set; }

        public bool Status { get; set; } = true;
    }
}
