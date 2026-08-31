using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class Subjekti
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Ime subjekta ne smije biti duži od 100 znakova")]
        public required string Ime { get; set; }

        [Required(ErrorMessage = "Prezime subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Prezime subjekta ne smije biti duži od 100 znakova")]
        public required string Prezime { get; set; }

        [Required(ErrorMessage = "Email subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Email subjekta ne smije biti duži od 100 znakova")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "OIB subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "OIB subjekta ne smije biti duži od 100 znakova")]
        public required string OIB { get; set; }

        [Display(Name = "Naziv tvrtke")]
        [StringLength(100, ErrorMessage = "Naziv tvrtke ne smije biti duži od 100 znakova")]
        public string NazivTvrtke { get; set; } = null!;

        [Required(ErrorMessage = "Mobitel subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Mobitel subjekta ne smije biti duži od 100 znakova")]
        public required string Mobitel { get; set; }
    }
}
