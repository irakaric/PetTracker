using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class DetaljiMikrocipaLjubimaca
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentifikacijskeOznakeZivotinje")]
        public int IdentifikacijskaOznakaZivotinjeId { get; set; }
        public virtual IdentifikacijskeOznakeZivotinje? IdentifikacijskeOznakeZivotinje  { get; set; }

        [Display(Name = "Mjesto implantacije")]
        [Required(ErrorMessage = "Mjesto implantacije je obavezno polje")]
        [StringLength(100, ErrorMessage = "Mjesto implantacije ne smije biti duže od 100 znakova")]
        public required string MjestoImplantacije { get; set; }
    }
}
