using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZavrsniRad.Models;
using ZavrsniRad.Models.Enums;

namespace ZavrsniRad.Controllers
{
    public class AccountController : Controller
    {
        private readonly PetTrackerDbContext _context;

        public AccountController(PetTrackerDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string korisnickoIme, string lozinka)
        {
            KorisniciAplikacije? korisnik = await _context.KorisniciAplikacije
                .FirstOrDefaultAsync(x =>
                    x.Username == korisnickoIme &&
                    x.Password == lozinka &&
                    x.Status);

            if (korisnik == null)
            {
                ModelState.AddModelError(string.Empty,
                    "Neispravno korisničko ime ili lozinka");
                return View();
            }

            int rasporedId = await _context.SubjektiRasporedi
                .Where(sr => sr.KorisniciAplikacijeId == korisnik.Id && sr.Status)
                .OrderByDescending(sr => sr.AktivanRaspored)
                .Select(sr => sr.Id)
                .FirstOrDefaultAsync();

            if (rasporedId == 0)
            {
                ModelState.AddModelError(string.Empty,
                    "Korisnik nema dodijeljen aktivan subjekt");
                return View();
            }

            List<string> role = await _context.SubjektiRasporediRole
                .Where(srr => srr.RasporedSubjektaId == rasporedId)
                .Select(srr => srr.Role!.Naziv)
                .ToListAsync();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, korisnik.Username)
            };

            claims.AddRange(role.Select(r => new Claim(ClaimTypes.Role, r)));

            ClaimsIdentity identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties =
                new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                properties);

            return RedirectToAction("Index", "Subjekti");
        }

        [Authorize]
        public async Task<IActionResult> Odjava()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = RoleNazivi.Korisnik)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromijeniProfil(int rasporedId)
        {
            string? korisnickoIme = User.Identity?.Name;

            int korisnikId = await _context.KorisniciAplikacije
                .Where(k => k.Username == korisnickoIme)
                .Select(k => k.Id)
                .FirstOrDefaultAsync();

            if (korisnikId == 0)
            {
                return NotFound();
            }

            List<SubjektiRasporedi> rasporedi = await _context.SubjektiRasporedi
                .Where(sr => sr.KorisniciAplikacijeId == korisnikId && sr.Status)
                .ToListAsync();

            if (!rasporedi.Any(sr => sr.Id == rasporedId))
            {
                return Forbid();
            }

            foreach (SubjektiRasporedi raspored in rasporedi)
            {
                raspored.AktivanRaspored = raspored.Id == rasporedId;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Subjekti");
        }

        [AllowAnonymous]
        public IActionResult Registracija()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registracija(SubjektiRasporedi korisnikSubjekt)
        {
            if (korisnikSubjekt.KorisniciAplikacije == null || korisnikSubjekt.Subjekti == null)
            {
                ModelState.AddModelError(string.Empty,
                    "Potrebno je unijeti podatke o subjektu i podatke za prijavu");
                return View(korisnikSubjekt);
            }

            string korisnickoIme =
                (korisnikSubjekt.KorisniciAplikacije.Username ?? string.Empty).Trim();

            korisnikSubjekt.KorisniciAplikacije.Username = korisnickoIme;

            if (await _context.KorisniciAplikacije
                    .AnyAsync(x => x.Status && x.Username == korisnickoIme))
            {
                ModelState.AddModelError("KorisniciAplikacije.Username",
                    "Korisničko ime je već zauzeto");
            }

            if (await _context.Subjekti
                    .AnyAsync(x => x.Ime == korisnikSubjekt.Subjekti.Ime
                                && x.Prezime == korisnikSubjekt.Subjekti.Prezime))
            {
                ModelState.AddModelError("Subjekti.Ime",
                    "Subjekt s navedenim imenom i prezimenom već postoji");
            }

            if (!ModelState.IsValid)
            {
                return View(korisnikSubjekt);
            }

            korisnikSubjekt.KorisniciAplikacije.Status = true;

            korisnikSubjekt.Subjekti.VrstaOsobeId = (int)VrstaOsobe.FizickaOsoba;
            korisnikSubjekt.Subjekti.VrstaSubjektaId = (int)VrstaSubjekta.Gradanin;
            korisnikSubjekt.Subjekti.Status = true;

            korisnikSubjekt.Status = true;

            _context.SubjektiRasporedi.Add(korisnikSubjekt);
            await _context.SaveChangesAsync();

            _context.SubjektiRasporediRole.Add(new SubjektiRasporediRole
            {
                RasporedSubjektaId = korisnikSubjekt.Id,
                RolaId = (int)Rola.Korisnik
            });
            await _context.SaveChangesAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
