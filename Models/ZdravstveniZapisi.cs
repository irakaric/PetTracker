using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class ZdravstveniZapisi
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Zivotinje")]
        public int ZivotinjaId { get; set; }
        public virtual Zivotinje? Zivotinje { get; set; }

        [ForeignKey("ZdravstveniZapisiVrste")]
        public int VrstaZdravstvenogZapisaId { get; set; }
        public virtual ZdravstveniZapisiVrste? ZdravstveniZapisiVrste { get; set; }

        [Display(Name = "Datum pregleda")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Opis zdravstvenog zapisa je obavezan")]
        [StringLength(2000,ErrorMessage = "Opis ne smije biti duži od 2000 znakova")]
        public required string Opis { get; set; }

        public bool Status { get; set; } = true;
    }
}
