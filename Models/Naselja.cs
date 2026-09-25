using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class Naselja
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naselje")]
        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv ne smije biti duži od 50 znakova")]
        public required string Naziv { get; set; }

        [Display(Name = "Županija")]
        [ForeignKey("Zupanije")]
        public int? ZupanijaId { get; set; }
        public virtual Zupanije? Zupanije { get; set; }

        [Display(Name = "Poštanski broj")]
        [StringLength(5, ErrorMessage = "Poštanski broj ne smije biti duži od 5 znakova")]
        public required string PostanskiBroj { get; set; }

        [Display(Name = "Država")]
        [ForeignKey("Drzave")]
        public int? DrzavaId { get; set; }
        public virtual Drzave? Drzave { get; set; }

        public bool Status { get; set; } = true;
    }
}
