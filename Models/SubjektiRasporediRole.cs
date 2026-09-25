using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class SubjektiRasporediRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SubjektiRasporedi")]
        public int RasporedSubjektaId { get; set; }
        public virtual SubjektiRasporedi? SubjektiRasporedi { get; set; }

        [ForeignKey("Role")]
        public int RolaId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
