using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class Subjekti
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Ime subjekta ne smije biti duže od 100 znakova")]
        public required string Ime { get; set; }

        [Required(ErrorMessage = "Prezime subjekta je obavezno polje")]
        [StringLength(100, ErrorMessage = "Prezime subjekta ne smije biti duže od 100 znakova")]
        public required string Prezime { get; set; }

        [Display(Name = "Naziv tvrtke")]
        [StringLength(100, ErrorMessage = "Naziv tvrtke ne smije biti duži od 100 znakova")]
        public string? NazivTvrtke { get; set; }

        [StringLength(50, ErrorMessage = "Email subjekta ne smije biti duži od 50 znakova")]
        public string? Email { get; set; }

        [StringLength(20, ErrorMessage = "Telefon subjekta ne smije biti duži od 20 znakova")]
        public string? Telefon { get; set; }

        [Display(Name = "Vrsta osobe")]
        [ForeignKey("SubjektiOsobe")]
        public int VrstaOsobeId { get; set; }
        public virtual SubjektiOsobe? SubjektiOsobe { get; set; }

        [Display(Name = "Vrsta subjekta")]
        [ForeignKey("SubjektiVrste")]
        public int VrstaSubjektaId { get; set; }
        public virtual SubjektiVrste? SubjektiVrste { get; set; }

        public bool Status { get; set; } = true;

    }
}
