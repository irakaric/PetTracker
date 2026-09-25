using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class DetaljiTetovazeLjubimaca
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentifikacijskeOznakeZivotinje")]
        public int IdentifikacijskaOznakaZivotinjeId { get; set; }
        public virtual IdentifikacijskeOznakeZivotinje? IdentifikacijskeOznakeZivotinje { get; set; }

        [Display(Name = "Položaj tetovaže")]
        [Required(ErrorMessage = "Položaj tetovaže je obavezno polje")]
        [StringLength(50, ErrorMessage = "Položaj tetovaže ne smije biti duže od 50 znakova")]
        public required string PolozajTetovaze { get; set; }

        [Required(ErrorMessage = "Boja je obavezno polje")]
        [StringLength(50, ErrorMessage = "Boja ne smije biti duže od 50 znakova")]
        public required string Boja { get; set; }

        [StringLength(500, ErrorMessage = "Opis ne smije biti duži od 500 znakova")]
        public string? Opis { get; set; }
    }
}
