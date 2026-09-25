
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZavrsniRad.Models;
using ZavrsniRad.Models.Enums;
using ZavrsniRad.Models.ViewModels;

public class SubjektiController : Controller
{
    private readonly PetTrackerDbContext _context;

    public SubjektiController(PetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (!JeAdministrator())
        {
            int subjektId = await DohvatiPrivatniSubjektKorisnika();

            if (subjektId == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), new { id = subjektId });
        }

        await PripremiSifrarnike();
        return View(await DohvatiSubjekte());
    }

    private bool JeAdministrator()
    {
        return User.IsInRole(RoleNazivi.AdministratorSustava);
    }

    private async Task<bool> SmijePristupitiSubjektu(int subjektId)
    {
        if (JeAdministrator())
        {
            return false;
        }

        List<int> subjektiKorisnika = await DohvatiSubjekteKorisnika();

        return subjektiKorisnika.Contains(subjektId);
    }

    private async Task<List<int>> DohvatiSubjekteKorisnika()
    {
        int korisnikId = await DohvatiKorisnikId();

        return await _context.SubjektiRasporedi
            .Where(sr => sr.KorisniciAplikacijeId == korisnikId && sr.Status)
            .Select(sr => sr.SubjektId)
            .ToListAsync();
    }

    private async Task<int> DohvatiPrivatniSubjektKorisnika()
    {
        int korisnikId = await DohvatiKorisnikId();

        return await _context.SubjektiRasporedi
            .Where(sr => sr.KorisniciAplikacijeId == korisnikId
                && sr.Status
                && sr.Subjekti!.VrstaSubjektaId == (int)VrstaSubjekta.Gradanin)
            .Select(sr => sr.SubjektId)
            .FirstOrDefaultAsync();
    }

    private async Task<int> DohvatiKorisnikId()
    {
        string? korisnickoIme = User.Identity?.Name;

        return await _context.KorisniciAplikacije
            .Where(k => k.Username == korisnickoIme)
            .Select(k => k.Id)
            .FirstOrDefaultAsync();
    }

    private async Task<List<SubjektPopisViewModel>> DohvatiSubjekte()
    {
        int prijavljeniKorisnikId = await DohvatiKorisnikId();

        var subjekti = await _context.Subjekti
            .Include(s => s.SubjektiOsobe)
            .Include(s => s.SubjektiVrste)
            .OrderBy(s => s.Prezime)
            .ThenBy(s => s.Ime)
            .ToListAsync();

        var rasporedi = await _context.SubjektiRasporedi
            .Where(sr => sr.Status)
            .Select(sr => new
            {
                sr.Id,
                sr.SubjektId,
                sr.KorisniciAplikacijeId
            })
            .ToListAsync();

        var rasporediRole = await _context.SubjektiRasporediRole
            .Select(srr => new
            {
                srr.RasporedSubjektaId,
                Naziv = srr.Role!.Naziv
            })
            .ToListAsync();

        return subjekti
            .Select(s =>
            {
                var rasporediSubjekta = rasporedi
                    .Where(sr => sr.SubjektId == s.Id)
                    .ToList();

                var role = rasporediSubjekta
                    .SelectMany(sr => rasporediRole
                        .Where(srr => srr.RasporedSubjektaId == sr.Id)
                        .Select(srr => srr.Naziv))
                    .Distinct()
                    .OrderBy(naziv => naziv)
                    .ToList();

                return new SubjektPopisViewModel
                {
                    Subjekt = s,
                    Role = string.Join(", ", role),
                    JePrijavljeniAdministrator =
                        role.Contains(RoleNazivi.AdministratorSustava)
                        && rasporediSubjekta.Any(sr => sr.KorisniciAplikacijeId == prijavljeniKorisnikId)
                };
            })
            .ToList();
    }

    private async Task PripremiSifrarnike(int? vrstaOsobeId = null, int? vrstaSubjektaId = null)
    {
        ViewBag.VrsteOsoba = new SelectList(
            await _context.SubjektiOsobe
                .Where(o => o.Status)
                .OrderBy(o => o.Naziv)
                .ToListAsync(),
            "Id", "Naziv", vrstaOsobeId);

        ViewBag.VrsteSubjekata = new SelectList(
            await _context.SubjektiVrste
                .Where(v => v.Status)
                .OrderBy(v => v.Naziv)
                .ToListAsync(),
            "Id", "Naziv", vrstaSubjektaId);
    }

    private async Task PripremiSifrarnikeAdresa(int? vrstaAdreseId = null, int? naseljeId = null)
    {
        ViewBag.VrsteAdresa = new SelectList(
            await _context.VrsteAdresa
                .Where(v => v.Status)
                .OrderBy(v => v.Naziv)
                .ToListAsync(),
            "Id", "Naziv", vrstaAdreseId);

        ViewBag.Naselja = new SelectList(
            await _context.Naselja
                .Where(n => n.Status)
                .OrderBy(n => n.Naziv)
                .ToListAsync(),
            "Id", "Naziv", naseljeId);
    }

    private async Task PripremiAdrese(int subjektId)
    {
        ViewBag.Adrese = await DohvatiAdrese(subjektId);
    }

    private async Task<List<SubjektiAdresa>> DohvatiAdrese(int subjektId)
    {
        return await _context.SubjektiAdrese
            .Where(a => a.SubjektId == subjektId)
            .Include(a => a.VrstaAdresa)
            .Include(a => a.Naselja!).ThenInclude(n => n.Zupanije)
            .Include(a => a.Naselja!).ThenInclude(n => n.Drzave)
            .OrderBy(a => a.VrstaAdresa!.Naziv)
            .ToListAsync();
    }

    private async Task PripremiSifrarnikePoslovnogProfila()
    {
        ViewBag.PravniOblici = await _context.PravniOblici
            .Where(p => p.Status)
            .OrderBy(p => p.Naziv)
            .ToListAsync();

        ViewBag.Djelatnosti = await _context.Djelatnosti
            .Where(d => d.Status)
            .OrderBy(d => d.Naziv)
            .ToListAsync();
    }

    private async Task<Subjekti?> DohvatiPoslovniProfil(int privatniSubjektId)
    {
        int korisnikId = await DohvatiKorisnikId();

        if (korisnikId == 0)
        {
            return null;
        }

        int poslovniSubjektId = await _context.SubjektiRasporedi
            .Where(sr => sr.KorisniciAplikacijeId == korisnikId
                && sr.Status
                && sr.SubjektId != privatniSubjektId
                && sr.Subjekti!.VrstaSubjektaId != (int)VrstaSubjekta.Gradanin)
            .Select(sr => sr.SubjektId)
            .FirstOrDefaultAsync();

        if (poslovniSubjektId == 0)
        {
            return null;
        }

        return await _context.Subjekti
            .Include(s => s.SubjektiOsobe)
            .Include(s => s.SubjektiVrste)
            .FirstOrDefaultAsync(s => s.Id == poslovniSubjektId && s.Status);
    }

    private async Task PripremiPoslovniProfil(int privatniSubjektId)
    {
        if (JeAdministrator())
        {
            return;
        }

        await PripremiSifrarnikePoslovnogProfila();

        Subjekti? poslovniProfil = await DohvatiPoslovniProfil(privatniSubjektId);

        if (poslovniProfil == null)
        {
            return;
        }

        ViewBag.PoslovniProfil = poslovniProfil;

        ViewBag.PoslovniProfilDetalji = await _context.SubjektiDetalji
            .Include(d => d.PravniOblici)
            .Include(d => d.Djelatnosti)
            .FirstOrDefaultAsync(d => d.SubjektId == poslovniProfil.Id);

        ViewBag.PoslovneAdrese = await DohvatiAdrese(poslovniProfil.Id);
    }

    public async Task<IActionResult> Edit(int? id, string? tab = null)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subjekti = await _context.Subjekti.FindAsync(id);

        if (subjekti == null)
        {
            return NotFound();
        }

        if (!await SmijePristupitiSubjektu(subjekti.Id))
        {
            return Forbid();
        }

        ViewBag.OtvoriTab = tab;

        await PripremiSifrarnike(subjekti.VrstaOsobeId, subjekti.VrstaSubjektaId);
        await PripremiSifrarnikeAdresa();
        await PripremiAdrese(subjekti.Id);
        await PripremiPoslovniProfil(subjekti.Id);

        return View(subjekti);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Ime,Prezime,Email,NazivTvrtke,Telefon,VrstaOsobeId,VrstaSubjektaId,Status")] Subjekti subjekti)
    {
        if (id != subjekti.Id)
        {
            return NotFound();
        }

        if (!await SmijePristupitiSubjektu(subjekti.Id))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subjekti);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjektiExists(subjekti.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Edit), new { id = subjekti.Id });
        }

        await PripremiSifrarnike(subjekti.VrstaOsobeId, subjekti.VrstaSubjektaId);
        await PripremiSifrarnikeAdresa();
        await PripremiAdrese(subjekti.Id);
        await PripremiPoslovniProfil(subjekti.Id);

        return View(subjekti);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = RoleNazivi.AdministratorSustava)]
    public async Task<IActionResult> Obrisi(int? id)
    {
        var subjekt = await _context.Subjekti.FindAsync(id);

        if (subjekt == null)
        {
            return NotFound();
        }

        var rasporedi = await _context.SubjektiRasporedi
            .Where(sr => sr.SubjektId == subjekt.Id)
            .ToListAsync();

        int prijavljeniKorisnikId = await DohvatiKorisnikId();

        List<int> rasporedIds = rasporedi.Select(sr => sr.Id).ToList();

        bool jeAdministratorSubjekt = await _context.SubjektiRasporediRole
            .AnyAsync(srr => rasporedIds.Contains(srr.RasporedSubjektaId)
                && srr.Role!.Naziv == RoleNazivi.AdministratorSustava);

        if (jeAdministratorSubjekt
            && rasporedi.Any(sr => sr.KorisniciAplikacijeId == prijavljeniKorisnikId))
        {
            TempData["GreskaBrisanjaSubjekta"] =
                "Nije moguÄ‡e obrisati subjekt administratora sustava koji je trenutno prijavljen.";

            return RedirectToAction(nameof(Index));
        }

        subjekt.Status = false;

        foreach (var raspored in rasporedi)
        {
            raspored.AktivanRaspored = false;
            raspored.Status = false;
        }

        List<int> korisnikIds = rasporedi
            .Select(sr => sr.KorisniciAplikacijeId)
            .Distinct()
            .ToList();

        var korisnici = await _context.KorisniciAplikacije
            .Where(k => korisnikIds.Contains(k.Id))
            .ToListAsync();

        foreach (var korisnik in korisnici)
        {
            korisnik.Status = false;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DodajAdresu([Bind("Id,SubjektId,Ulica,KucniBroj,VrstaAdreseId,NaseljeId")] SubjektiAdresa adresa, int? povratniSubjektId)
    {
        if (!SubjektiExists(adresa.SubjektId))
        {
            return NotFound();
        }

        int subjektFormeId = povratniSubjektId ?? adresa.SubjektId;

        if (!await SmijePristupitiSubjektu(adresa.SubjektId)
            || !await SmijePristupitiSubjektu(subjektFormeId))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            if (adresa.Id == 0)
            {
                _context.SubjektiAdrese.Add(adresa);
            }
            else
            {
                _context.SubjektiAdrese.Update(adresa);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit),
                new { id = subjektFormeId, tab = "adrese" });
        }

        var subjekti = await _context.Subjekti.FindAsync(subjektFormeId);

        if (subjekti == null)
        {
            return NotFound();
        }

        await PripremiSifrarnike(subjekti.VrstaOsobeId, subjekti.VrstaSubjektaId);
        await PripremiSifrarnikeAdresa(adresa.VrstaAdreseId, adresa.NaseljeId);
        await PripremiAdrese(subjekti.Id);
        await PripremiPoslovniProfil(subjekti.Id);

        ViewBag.OtvoriAdresaModal = true;
        ViewBag.NovaAdresa = adresa;
        ViewBag.OtvoriTab = "adrese";

        return View(nameof(Edit), subjekti);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ObrisiAdresu(int? id, int? povratniSubjektId)
    {
        var adresa = await _context.SubjektiAdrese.FindAsync(id);

        if (adresa == null)
        {
            return NotFound();
        }

        if (!await SmijePristupitiSubjektu(adresa.SubjektId))
        {
            return Forbid();
        }

        int subjektFormeId = povratniSubjektId ?? adresa.SubjektId;

        _context.SubjektiAdrese.Remove(adresa);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Edit),
            new { id = subjektFormeId, tab = "adrese" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = RoleNazivi.Korisnik)]
    public async Task<IActionResult> DodajPoslovniProfil(int id, PoslovniProfilViewModel unos)
    {
        var privatniSubjekt = await _context.Subjekti.FindAsync(id);

        if (privatniSubjekt == null)
        {
            return NotFound();
        }

        if (!await SmijePristupitiSubjektu(privatniSubjekt.Id))
        {
            return Forbid();
        }

        if (await DohvatiPoslovniProfil(privatniSubjekt.Id) != null)
        {
            return RedirectToAction(nameof(Edit), new { id = privatniSubjekt.Id });
        }

        if (ModelState.IsValid)
        {
            int korisnikId = await DohvatiKorisnikId();

            if (korisnikId == 0)
            {
                return NotFound();
            }

            var poslovniSubjekt = new Subjekti
            {
                Ime = unos.Ime,
                Prezime = unos.Prezime,
                NazivTvrtke = unos.NazivTvrtke,
                Email = unos.Email,
                Telefon = unos.Telefon,
                VrstaOsobeId = unos.VrstaOsobeId,
                VrstaSubjektaId = unos.VrstaSubjektaId,
                Status = true
            };

            _context.Subjekti.Add(poslovniSubjekt);
            await _context.SaveChangesAsync();

            _context.SubjektiDetalji.Add(new SubjektiDetalji
            {
                SubjektId = poslovniSubjekt.Id,
                OIB = unos.OIB,
                MB = unos.MB,
                MBO = unos.MBO,
                MBS = unos.MBS ?? string.Empty,
                DatumOsnivanja = unos.DatumOsnivanja,
                PravniOblikId = unos.PravniOblikId,
                DjelatnostId = unos.DjelatnostId
            });

            var raspored = new SubjektiRasporedi
            {
                KorisniciAplikacijeId = korisnikId,
                SubjektId = poslovniSubjekt.Id,
                AktivanRaspored = false,
                Status = true
            };

            _context.SubjektiRasporedi.Add(raspored);
            await _context.SaveChangesAsync();

            _context.SubjektiRasporediRole.Add(new SubjektiRasporediRole
            {
                RasporedSubjektaId = raspored.Id,
                RolaId = (int)Rola.Korisnik
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = privatniSubjekt.Id, tab = "adrese" });
        }

        await PripremiSifrarnike(privatniSubjekt.VrstaOsobeId, privatniSubjekt.VrstaSubjektaId);
        await PripremiSifrarnikeAdresa();
        await PripremiAdrese(privatniSubjekt.Id);
        await PripremiPoslovniProfil(privatniSubjekt.Id);

        ViewBag.OtvoriPoslovniProfilModal = true;
        ViewBag.PoslovniProfilUnos = unos;

        return View(nameof(Edit), privatniSubjekt);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = RoleNazivi.Korisnik)]
    public async Task<IActionResult> SpremiPoslovniProfil(int id, int poslovniSubjektId,
        string ime, string prezime, string? nazivTvrtke, string? email, string? telefon,
        string oib, string mb, string mbo, string? mbs, DateTime? datumOsnivanja)
    {
        var privatniSubjekt = await _context.Subjekti.FindAsync(id);

        if (privatniSubjekt == null)
        {
            return NotFound();
        }

        if (!await SmijePristupitiSubjektu(privatniSubjekt.Id))
        {
            return Forbid();
        }

        Subjekti? poslovniProfil = await DohvatiPoslovniProfil(privatniSubjekt.Id);

        if (poslovniProfil == null || poslovniProfil.Id != poslovniSubjektId)
        {
            return NotFound();
        }

        poslovniProfil.Ime = ime;
        poslovniProfil.Prezime = prezime;
        poslovniProfil.NazivTvrtke = nazivTvrtke;
        poslovniProfil.Email = email;
        poslovniProfil.Telefon = telefon;

        var detalji = await _context.SubjektiDetalji
            .FirstOrDefaultAsync(d => d.SubjektId == poslovniProfil.Id);

        if (detalji != null)
        {
            detalji.OIB = oib;
            detalji.MB = mb;
            detalji.MBO = mbo;
            detalji.MBS = mbs ?? string.Empty;
            detalji.DatumOsnivanja = datumOsnivanja;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Edit), new { id = privatniSubjekt.Id });
    }

    private bool SubjektiExists(int? id)
    {
        return _context.Subjekti.Any(e => e.Id == id);
    }
}
