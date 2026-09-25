using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class SubjektiRasporedi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Korisnik aplikacije")]
        [ForeignKey("KorisniciAplikacije")]
        public int KorisniciAplikacijeId { get; set; }
        public virtual KorisniciAplikacije? KorisniciAplikacije { get; set; }

        [Display(Name = "Subjekt")]
        [ForeignKey("Subjekti")]
        public int SubjektId { get; set; }
        public virtual Subjekti? Subjekti { get; set; }

        public bool AktivanRaspored { get; set; } = true;

        public bool Status { get; set; } = true;


    }
}
