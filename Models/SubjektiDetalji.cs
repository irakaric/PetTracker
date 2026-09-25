using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class SubjektiDetalji
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Subjekt")]
        [ForeignKey("Subjekti")]
        public int SubjektId { get; set; }
        public virtual Subjekti? Subjekti { get; set; }

        [Display(Name = "Datum osnivanja")]
        public DateTime? DatumOsnivanja { get; set; }

        [Required(ErrorMessage = "OIB subjekta je obavezno polje")]
        [StringLength(11, ErrorMessage = "OIB subjekta ne smije biti duži od 11 znakova")]
        public required string OIB { get; set; }

        [Display(Name = "Matični broj obrta")]
        [Required(ErrorMessage = "MBO subjekta je obavezno polje")]
        [StringLength(8, ErrorMessage = "MBO subjekta ne smije biti duži od 8 znakova")]
        public required string MBO { get; set; }

        [Display(Name = "Matični broj")]
        [Required(ErrorMessage = "MB subjekta je obavezno polje")]
        [StringLength(8, ErrorMessage = "MB subjekta ne smije biti duži od 8 znakova")]
        public required string MB { get; set; }

        [Display(Name = "Matični broj subjekta")]
        [Required(ErrorMessage = "MBS subjekta je obavezno polje")]
        [StringLength(9, ErrorMessage = "MBS subjekta ne smije biti duži od 9 znakova")]
        public required string MBS { get; set; }

        [Display(Name = "Pravni oblik")]
        [ForeignKey("PravniOblici")]
        public int PravniOblikId { get; set; }
        public virtual PravniOblici? PravniOblici { get; set; }

        [Display(Name = "Djelatnost")]
        [ForeignKey("Djelatnosti")]
        public int DjelatnostId { get; set; }
        public virtual Djelatnosti? Djelatnosti { get; set; }

    }
}
