using System.ComponentModel.DataAnnotations;

namespace ZavrsniRad.Models
{
    public class Drzave
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Država")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv ne smije biti duži od 50 znakova")]
        public required string Naziv { get; set; }


        [Required(ErrorMessage = "OznakaAlpha2 je obavezno polje")]
        [StringLength(50, ErrorMessage = "OznakaAlpha2 ne smije biti duža od 50 znakova")]
        public required string OznakaAlpha2 { get; set; }


        [Required(ErrorMessage = "OznakaAlpha3 je obavezno polje")]
        [StringLength(50, ErrorMessage = "OznakaAlpha3 ne smije biti duža od 50 znakova")]
        public required string OznakaAlpha3 { get; set; }


        [Required(ErrorMessage = "NumerickaOznaka je obavezno polje")]
        [StringLength(50, ErrorMessage = "NumerickaOznaka ne smije biti duža od 50 znakova")]
        public required string NumerickaOznaka { get; set; }

        public bool Status { get; set; } = true;

    }
}
