using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavrsniRad.Models;
using ZavrsniRad.Models.Enums;
using ZavrsniRad.Models.ViewModels;

namespace ZavrsniRad.ViewComponents
{
    public class PrebacivanjeProfilaViewComponent : ViewComponent
    {
        private readonly PetTrackerDbContext _context;

        public PrebacivanjeProfilaViewComponent(PetTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (UserClaimsPrincipal.IsInRole(RoleNazivi.AdministratorSustava)
                || !UserClaimsPrincipal.IsInRole(RoleNazivi.Korisnik))
            {
                return Content(string.Empty);
            }

            string? korisnickoIme = UserClaimsPrincipal.Identity?.Name;

            int korisnikId = await _context.KorisniciAplikacije
                .Where(k => k.Username == korisnickoIme)
                .Select(k => k.Id)
                .FirstOrDefaultAsync();

            if (korisnikId == 0)
            {
                return Content(string.Empty);
            }

            List<ProfilViewModel> profili = await _context.SubjektiRasporedi
                .Where(sr => sr.KorisniciAplikacijeId == korisnikId
                    && sr.Status
                    && sr.Subjekti!.Status)
                .Select(sr => new ProfilViewModel
                {
                    RasporedId = sr.Id,
                    SubjektId = sr.SubjektId,
                    Aktivan = sr.AktivanRaspored,
                    JePoslovni = sr.Subjekti!.VrstaSubjektaId != (int)VrstaSubjekta.Gradanin,
                    Naziv = sr.Subjekti!.VrstaSubjektaId == (int)VrstaSubjekta.Gradanin
                        ? sr.Subjekti!.Ime + " " + sr.Subjekti!.Prezime
                        : (sr.Subjekti!.NazivTvrtke ?? sr.Subjekti!.Ime + " " + sr.Subjekti!.Prezime)
                })
                .ToListAsync();

            if (profili.Count < 2)
            {
                return Content(string.Empty);
            }

            return View(profili.OrderBy(p => p.JePoslovni).ToList());
        }
    }
}
