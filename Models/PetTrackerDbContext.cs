using Microsoft.EntityFrameworkCore;

namespace ZavrsniRad.Models
{
    public class PetTrackerDbContext : DbContext
    {
        public PetTrackerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KorisniciAplikacije> KorisniciAplikacije { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SubjektiRasporedi> SubjektiRasporedi { get; set; }
        public DbSet<SubjektiRasporediRole> SubjektiRasporediRole { get; set; }

        public DbSet<Subjekti> Subjekti { get; set; }
        public DbSet<SubjektiDetalji> SubjektiDetalji { get; set; }
        public DbSet<SubjektiOsobe> SubjektiOsobe { get; set; }
        public DbSet<SubjektiVrste> SubjektiVrste { get; set; }
        public DbSet<SubjektiAdresa> SubjektiAdrese { get; set; }
        public DbSet<SubjektiZivotinje> SubjektiZivotinje { get; set; }
        public DbSet<Djelatnosti> Djelatnosti { get; set; }
        public DbSet<PravniOblici> PravniOblici { get; set; }

        public DbSet<Drzave> Drzave { get; set; }
        public DbSet<Zupanije> Zupanije { get; set; }
        public DbSet<Naselja> Naselja { get; set; }
        public DbSet<VrsteAdresa> VrsteAdresa { get; set; }

        public DbSet<Zivotinje> Zivotinje { get; set; }
        public DbSet<ZivotinjeVrste> ZivotinjeVrste { get; set; }
        public DbSet<Pasmine> Pasmine { get; set; }
        public DbSet<ZivotinjeVrstePasmina> ZivotinjeVrstePasmina { get; set; }
        public DbSet<Spol> Spol { get; set; }
        public DbSet<ZivotinjeStatusi> ZivotinjeStatusi { get; set; }
        public DbSet<Prehrana> Prehrana { get; set; }

        public DbSet<IdentifikacijskeOznakeZivotinje> IdentifikacijskeOznakeZivotinje { get; set; }
        public DbSet<IdentifikacijskeOznakeVrste> IdentifikacijskeOznakeVrste { get; set; }
        public DbSet<DetaljiMikrocipaLjubimaca> DetaljiMikrocipaLjubimaca { get; set; }
        public DbSet<DetaljiTetovazeLjubimaca> DetaljiTetovazeLjubimaca { get; set; }
        public DbSet<DetaljiPrstenaLjubimaca> DetaljiPrstenaLjubimaca { get; set; }
        public DbSet<TipPrstena> TipPrstena { get; set; }

        public DbSet<ZdravstveniZapisi> ZdravstveniZapisi { get; set; }
        public DbSet<ZdravstveniZapisiVrste> ZdravstveniZapisiVrste { get; set; }
    }

    
}
