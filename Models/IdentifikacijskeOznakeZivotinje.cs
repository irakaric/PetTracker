using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class IdentifikacijskeOznakeZivotinje
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Zivotinje")]
        public int ZivotinjaId { get; set; }
        public virtual Zivotinje? Zivotinje { get; set; }

        [ForeignKey("IdentifikacijskeOznakeVrste")]
        public int VrstaIdentifikacijskeOznakeId { get; set; }
        public virtual IdentifikacijskeOznakeVrste? IdentifikacijskeOznakeVrste { get; set; }

        [Required(ErrorMessage = "Oznaka je obavezno polje")]
        [StringLength(50, ErrorMessage = "Oznaka ne smije biti duža od 50 znakova")]
        public required string Oznaka { get; set; }

        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }

        [ForeignKey("Drzave")]
        public int? DrzavaId { get; set; }
        public virtual Drzave? Drzave { get; set; }

        [Display(Name = "Označavatelj")]
        [Required(ErrorMessage = "Označavatelj je obavezno polje")]
        [StringLength(50, ErrorMessage = "Označavatelj ne smije biti duži od 50 znakova")]
        public required string Oznacavatelj { get; set; }

        [StringLength(500, ErrorMessage = "Napomena ne smije biti duža od 500 znakova")]
        public string? Napomena { get; set; }

    }
}
