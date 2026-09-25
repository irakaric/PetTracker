using System.ComponentModel.DataAnnotations;
using ZavrsniRad.Models.Enums;

namespace ZavrsniRad.Models.ViewModels
{
    public class PoslovniProfilViewModel : IValidatableObject
    {
        [Display(Name = "Vrsta osobe")]
        [Range(1, int.MaxValue, ErrorMessage = "Vrsta osobe je obavezno polje")]
        public int VrstaOsobeId { get; set; }

        [Display(Name = "Vrsta subjekta")]
        [Range(1, int.MaxValue, ErrorMessage = "Vrsta subjekta je obavezno polje")]
        public int VrstaSubjektaId { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        [StringLength(100)]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obavezno polje")]
        [StringLength(100)]
        public string Prezime { get; set; } = string.Empty;

        [Display(Name = "Naziv tvrtke")]
        [StringLength(100)]
        public string? NazivTvrtke { get; set; }

        [EmailAddress(ErrorMessage = "Email adresa nije ispravna")]
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefon { get; set; }

        [Required(ErrorMessage = "OIB je obavezno polje")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "OIB mora sadrÅ¾avati 11 znamenki")]
        public string OIB { get; set; } = string.Empty;

        [Display(Name = "MatiÄni broj")]
        [Required(ErrorMessage = "MatiÄni broj je obavezno polje")]
        [StringLength(8, ErrorMessage = "MatiÄni broj ne smije biti duÅ¾i od 8 znakova")]
        public string MB { get; set; } = string.Empty;

        [Display(Name = "MatiÄni broj obrta")]
        [Required(ErrorMessage = "MatiÄni broj obrta je obavezno polje")]
        [StringLength(8, ErrorMessage = "MatiÄni broj obrta ne smije biti duÅ¾i od 8 znakova")]
        public string MBO { get; set; } = string.Empty;

        [Display(Name = "MatiÄni broj subjekta")]
        [StringLength(9, ErrorMessage = "MatiÄni broj subjekta ne smije biti duÅ¾i od 9 znakova")]
        public string? MBS { get; set; }

        [Display(Name = "Datum osnivanja")]
        [DataType(DataType.Date)]
        public DateTime? DatumOsnivanja { get; set; }

        [Display(Name = "Pravni oblik")]
        [Range(1, int.MaxValue, ErrorMessage = "Pravni oblik je obavezno polje")]
        public int PravniOblikId { get; set; }

        [Display(Name = "Djelatnost")]
        [Range(1, int.MaxValue, ErrorMessage = "Djelatnost je obavezno polje")]
        public int DjelatnostId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VrstaOsobeId == (int)VrstaOsobe.FizickaOsoba &&
                VrstaSubjektaId != (int)VrstaSubjekta.Obrt)
            {
                yield return new ValidationResult(
                    "Za fiziÄku osobu vrsta subjekta mora biti Obrt.",
                    new[] { nameof(VrstaSubjektaId) });
            }

            if (VrstaOsobeId == (int)VrstaOsobe.PravnaOsoba &&
                VrstaSubjektaId != (int)VrstaSubjekta.TrgovackoDrustvo)
            {
                yield return new ValidationResult(
                    "Za pravnu osobu vrsta subjekta mora biti TrgovaÄko druÅ¡tvo.",
                    new[] { nameof(VrstaSubjektaId) });
            }

            if (VrstaOsobeId == (int)VrstaOsobe.PravnaOsoba &&
                string.IsNullOrWhiteSpace(MBS))
            {
                yield return new ValidationResult(
                    "MatiÄni broj subjekta je obavezno polje",
                    new[] { nameof(MBS) });
            }

            if (VrstaOsobeId == (int)VrstaOsobe.FizickaOsoba &&
                PravniOblikId != (int)PravniOblik.ZakonOObrtu)
            {
                yield return new ValidationResult(
                    "Za obrt pravni oblik mora biti Zakon o obrtu.",
                    new[] { nameof(PravniOblikId) });
            }

            if (VrstaOsobeId == (int)VrstaOsobe.PravnaOsoba &&
                PravniOblikId != (int)PravniOblik.DrustvoSOgranicenomOdgovornoscu)
            {
                yield return new ValidationResult(
                    "Za trgovaÄko druÅ¡tvo pravni oblik mora biti d.o.o.",
                    new[] { nameof(PravniOblikId) });
            }
        }
    }
}
