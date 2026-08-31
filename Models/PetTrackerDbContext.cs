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
        public DbSet<KorisniciAplikacijeRole> KorisniciAplikacijeRole { get; set; } 
        public DbSet<KorisniciAplikacijeSubjekti> KorisniciAplikacijeSubjekti { get; set; }
        public DbSet<Subjekti> Subjekti { get; set; }
    }
}
