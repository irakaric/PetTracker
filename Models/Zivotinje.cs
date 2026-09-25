using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class Zivotinje
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naziv životinje")]
        [Required(ErrorMessage = "Naziv životinje je obavezno polje")]
        [StringLength(50, ErrorMessage = "Naziv životinje ne smije biti duže od 50 znakova")]
        public required string Naziv { get; set; }

        [ForeignKey("ZivotinjeVrstePasmina")]
        public int VrstaZivotinjePasminaId { get; set; }
        public virtual ZivotinjeVrstePasmina? ZivotinjeVrstePasmina { get; set; }

        [ForeignKey("Spol")]
        public int SpolId { get; set; }
        public virtual Spol? Spol { get; set; }

        [StringLength(50, ErrorMessage = "Boja ne smije biti duža od 50 znakova")]
        public string? Boja { get; set; }

        [Display(Name = "Datum rođenja")]
        public DateTime? DatumRodenja { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("ZivotinjeStatusi")]
        public int StatusId { get; set; }
        public virtual ZivotinjeStatusi? ZivotinjeStatusi { get; set; }

    }
}
