using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class SubjektiAdresa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Subjekt")]
        [ForeignKey("Subjekti")]
        public int SubjektId { get; set; }
        public virtual Subjekti? Subjekti { get; set; }

        [Required(ErrorMessage = "Ulica je obavezno polje")]
        [StringLength(100, ErrorMessage = "Ulica ne smije biti duža od 100 znakova")]
        public required string Ulica { get; set; }

        [Display(Name = "Kućni broj")]
        [Required(ErrorMessage = "Kućni broj je obavezno polje")]
        [StringLength(10, ErrorMessage = "Kućni broj ne smije biti duži od 10 znakova")]
        public required string KucniBroj { get; set; }

        [Display(Name = "Vrsta adrese")]
        [ForeignKey("VrstaAdresa")]
        public int VrstaAdreseId { get; set; }
        public virtual VrsteAdresa? VrstaAdresa { get; set; }

        [Display(Name = "Naselje")]
        [ForeignKey("Naselja")]
        public int NaseljeId { get; set; }
        public virtual Naselja? Naselja { get; set; }
    }


}
