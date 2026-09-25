using ZavrsniRad.Models.Enums;

namespace ZavrsniRad.Models
{
    public static class OznakePoVrstiZivotinje
    {
        public static List<VrstaIdentifikacijskeOznake> DohvatiOznake(int? vrstaZivotinjeId)
        {
            return vrstaZivotinjeId switch
            {
                (int)VrstaZivotinje.Pas or (int)VrstaZivotinje.Macka =>
                    new List<VrstaIdentifikacijskeOznake>
                    {
                        VrstaIdentifikacijskeOznake.Mikrocip,
                        VrstaIdentifikacijskeOznake.Putovnica
                    },

                (int)VrstaZivotinje.Ptica =>
                    new List<VrstaIdentifikacijskeOznake>
                    {
                        VrstaIdentifikacijskeOznake.Prsten
                    },

                (int)VrstaZivotinje.Svinja =>
                    new List<VrstaIdentifikacijskeOznake>
                    {
                        VrstaIdentifikacijskeOznake.Tetovaza
                    },

                _ => new List<VrstaIdentifikacijskeOznake>()
            };
        }

        public static bool JeDozvoljena(int? vrstaZivotinjeId, int vrstaOznakeId)
        {
            return DohvatiOznake(vrstaZivotinjeId)
                .Any(o => (int)o == vrstaOznakeId);
        }

        public static string OznakaLabela(VrstaIdentifikacijskeOznake vrsta)
        {
            return vrsta switch
            {
                VrstaIdentifikacijskeOznake.Mikrocip => "Broj mikroÄipa",
                VrstaIdentifikacijskeOznake.Prsten => "Broj prstena",
                VrstaIdentifikacijskeOznake.Tetovaza => "Broj tetovaÅ¾e",
                VrstaIdentifikacijskeOznake.Putovnica => "Broj putovnice",
                _ => "Oznaka"
            };
        }

        public static string OznacavateljLabela(VrstaIdentifikacijskeOznake vrsta)
        {
            return vrsta switch
            {
                VrstaIdentifikacijskeOznake.Mikrocip => "Veterinar koji je Äipirao",
                VrstaIdentifikacijskeOznake.Prsten => "UzgajivaÄ koji je oznaÄio",
                VrstaIdentifikacijskeOznake.Tetovaza => "Osoba koja je tetovirala",
                VrstaIdentifikacijskeOznake.Putovnica => "Izdavatelj putovnice",
                _ => "OznaÄavatelj"
            };
        }

        public static string NazivTaba(VrstaIdentifikacijskeOznake vrsta)
        {
            return vrsta switch
            {
                VrstaIdentifikacijskeOznake.Mikrocip => "MikroÄip",
                VrstaIdentifikacijskeOznake.Prsten => "Prsten",
                VrstaIdentifikacijskeOznake.Tetovaza => "TetovaÅ¾a",
                VrstaIdentifikacijskeOznake.Putovnica => "Putovnica",
                _ => "Oznaka"
            };
        }

        public static string IdTaba(VrstaIdentifikacijskeOznake vrsta)
        {
            return "oznaka" + (int)vrsta;
        }
    }
}
