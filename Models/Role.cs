using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv ne smije biti duže od 50 znakova")]
        public required string Naziv { get; set; }
    }
}
