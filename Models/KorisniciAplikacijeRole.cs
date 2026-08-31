using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class KorisniciAplikacijeRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("KorisniciAplikacije")]
        public int KorisnikAplikacijeId { get; set; }
        public virtual KorisniciAplikacije? KorisniciAplikacije { get; set; }

        [ForeignKey("Role")]
        public int RolaId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
