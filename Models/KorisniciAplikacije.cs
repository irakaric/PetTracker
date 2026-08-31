using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class KorisniciAplikacije
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno polje")]
        [StringLength(50, ErrorMessage = "Korisničko ime ne smije biti duže od 50 znakova")]
        public required string Username { get; set; }
        
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [StringLength(50, ErrorMessage = "Lozinka ne smije biti duža od 50 znakova")]
        public required string Password { get; set; }

        public bool Status { get; set; } = true;
    }
}
