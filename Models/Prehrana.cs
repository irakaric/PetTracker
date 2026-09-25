using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZavrsniRad.Models
{
    public class Prehrana
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Zivotinje")]
        public int ZivotinjaId { get; set; }
        public virtual Zivotinje? Zivotinje { get; set; }

        [Display(Name = "Naziv prehrane")]
        [Required(ErrorMessage = "Naziv prehrane je obavezno polje")]
        [StringLength(100,ErrorMessage = "Naziv prehrane ne smije biti duži od 100 znakova")]
        public required string Naziv { get; set; }

        [Display(Name = "Vrsta hrane")]
        [StringLength(100,ErrorMessage = "Vrsta hrane ne smije biti duža od 100 znakova")]
        public string? VrstaHrane { get; set; }

        [Display(Name = "Naziv hrane ili proizvoda")]
        [StringLength(150,ErrorMessage = "Naziv hrane ili proizvoda ne smije biti duži od 150 znakova")]
        public string? NazivHrane { get; set; }

        [Display(Name = "Proizvođač hrane")]
        [StringLength(100,ErrorMessage = "Proizvođač hrane ne smije biti duži od 100 znakova")]
        public string? Proizvodac { get; set; }

        [Display(Name = "Količina hrane")]
        [StringLength(100,ErrorMessage = "Količina hrane ne smije biti duža od 100 znakova")]
        public string? KolicinaHrane { get; set; }

        [Display(Name = "Broj obroka dnevno")]
        public int? BrojObrokaDnevno { get; set; }

        [Display(Name = "Namirnice koje treba izbjegavati")]
        [StringLength(1000,ErrorMessage = "Namirnice koje treba izbjegavati ne smiju biti duže od 1000 znakova")]
        public string? NamirniceZaIzbjegavanje { get; set; }

        [Display(Name = "Napomena")]
        [StringLength(2000,ErrorMessage = "Napomena ne smije biti duža od 2000 znakova")]
        public string? Napomena { get; set; }

        public bool Status { get; set; } = true;


    }
}
