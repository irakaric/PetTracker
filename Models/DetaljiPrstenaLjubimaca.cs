using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class DetaljiPrstenaLjubimaca
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentifikacijskeOznakeZivotinje")]
        public int IdentifikacijskaOznakaZivotinjeId { get; set; }
        public virtual IdentifikacijskeOznakeZivotinje? IdentifikacijskeOznakeZivotinje { get; set; }

        [ForeignKey("TipPrstena")]
        public int TipPrstenaId { get; set; }
        public virtual TipPrstena? TipPrstena { get; set; }

        [Required(ErrorMessage = "Noga je obavezno polje")]
        [StringLength(10, ErrorMessage = "Noga ne smije biti duže od 10 znakova")]
        public required string Noga { get; set; }

        [Required(ErrorMessage = "Boja je obavezno polje")]
        [StringLength(20, ErrorMessage = "Boja ne smije biti duže od 20 znakova")]
        public required string Boja { get; set; }

        [Required(ErrorMessage = "Materijal je obavezno polje")]
        [StringLength(50, ErrorMessage = "Materijal ne smije biti duže od 50 znakova")]
        public required string Materijal { get; set; }

        [Display(Name = "Unutarnji promjer")]
        [Required(ErrorMessage = "Unutarnji promjer je obavezno polje")]
        [StringLength(50, ErrorMessage = "Unutarnji promjer ne smije biti duži od 50 znakova")]
        public required string UnutarnjiPromjer { get; set; }

        [Display(Name = "Godina na prstenu")]
        public int Godina { get; set; }

    }
}
