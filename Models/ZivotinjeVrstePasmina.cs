using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class ZivotinjeVrstePasmina
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ZivotinjeVrste")]
        public int VrstaZivotinjeId { get; set; }
        public virtual ZivotinjeVrste? ZivotinjeVrste { get; set; }

        [ForeignKey("Pasmina")]
        public int PasminaId { get; set; }
        public virtual Pasmine? Pasmina { get; set; }
    }
}
