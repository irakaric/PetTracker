using ZavrsniRad.Models.Enums;

namespace ZavrsniRad.Models.ViewModels
{
    public class IdentifikacijskaOznakaViewModel
    {
        public VrstaIdentifikacijskeOznake Vrsta { get; set; }

        public IdentifikacijskeOznakeZivotinje? Oznaka { get; set; }

        public DetaljiMikrocipaLjubimaca? DetaljiMikrocipa { get; set; }

        public DetaljiPrstenaLjubimaca? DetaljiPrstena { get; set; }

        public DetaljiTetovazeLjubimaca? DetaljiTetovaze { get; set; }

        public string NazivTaba => OznakePoVrstiZivotinje.NazivTaba(Vrsta);

        public string IdTaba => OznakePoVrstiZivotinje.IdTaba(Vrsta);

        public string OznakaLabela => OznakePoVrstiZivotinje.OznakaLabela(Vrsta);

        public string OznacavateljLabela => OznakePoVrstiZivotinje.OznacavateljLabela(Vrsta);
    }
}
