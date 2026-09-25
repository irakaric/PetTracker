using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class SubjektiZivotinje
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Subjekti")]
        public int SubjektId { get; set; }
        public virtual Subjekti? Subjekti { get; set; }

        [ForeignKey("Zivotinje")]
        public int ZivotinjaId { get; set; }
        public virtual Zivotinje? Zivotinje { get; set; } = null;


    }
}
