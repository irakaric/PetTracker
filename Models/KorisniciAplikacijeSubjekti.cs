using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class KorisniciAplikacijeSubjekti
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("KorisniciAplikacije")]
        public int KorisnikAplikacijeId { get; set; }
        public virtual KorisniciAplikacije? KorisniciAplikacije { get; set; }

        [ForeignKey("Subjekti")]
        public int SubjektId { get; set; }
        public virtual Subjekti? Subjekti { get; set; }
    }
}
