
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZavrsniRad.Models;
using ZavrsniRad.Models.Enums;
using ZavrsniRad.Models.ViewModels;

public class LjubimciController : Controller
{
    private readonly PetTrackerDbContext _context;

    public LjubimciController(PetTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()    
    {
        await PripremiSifrarnike();
        return View(await DohvatiLjubimce());
    }

    private async Task<int> DohvatiSubjektId()
    {
        string? korisnickoIme = User.Identity?.Name;

        int korisnikId = await _context.KorisniciAplikacije
            .Where(k => k.Username == korisnickoIme)
            .Select(k => k.Id)
            .FirstOrDefaultAsync();

        return await _context.SubjektiRasporedi
            .Where(ks => ks.KorisniciAplikacijeId == korisnikId && ks.Status)
            .OrderByDescending(ks => ks.AktivanRaspored)
            .Select(ks => ks.SubjektId)
            .FirstOrDefaultAsync();
    }

    private async Task<List<Zivotinje>> DohvatiLjubimce()
    {
        int subjektId = await DohvatiSubjektId();

        var ljubimciList = await _context.SubjektiZivotinje
            .Where(sl => sl.SubjektId == subjektId)
            .Select(sl => sl.ZivotinjaId)
            .ToListAsync();

        return await _context.Zivotinje
            .Where(l => ljubimciList.Contains(l.Id))
            .Include(l => l.ZivotinjeVrstePasmina!).ThenInclude(v => v.Pasmina)
            .Include(l => l.ZivotinjeVrstePasmina!).ThenInclude(v => v.ZivotinjeVrste)
            .Include(l => l.Spol)
            .Include(l => l.ZivotinjeStatusi)
            .ToListAsync();
    }

    private async Task PripremiSifrarnike(int? vrstaZivotinjaId = null)
    {
        ViewBag.VrsteZivotinja = new SelectList(
            await _context.ZivotinjeVrste
                .Where(v => v.Status)
                .OrderBy(v => v.Naziv)
                .ToListAsync(),
            "Id", "Naziv", vrstaZivotinjaId);

        ViewBag.Spol = new SelectList(
            await _context.Spol.ToListAsync(), "Id", "Naziv");

        ViewBag.StatusLjubimca = new SelectList(
            await _context.ZivotinjeStatusi.ToListAsync(), "Id", "Naziv");
    }

    [HttpGet]
    public async Task<IActionResult> PasminePoVrsti(int vrstaZivotinjaId)
    {
        var pasmine = await _context.ZivotinjeVrstePasmina
            .Where(v => v.VrstaZivotinjeId == vrstaZivotinjaId)
            .Include(v => v.Pasmina)
            .OrderBy(v => v.Pasmina!.Naziv)
            .Select(v => new
            {
                id = v.Id,
                naziv = v.Pasmina!.Naziv
            })
            .ToListAsync();

        return Json(pasmine);
    }

    public IActionResult Create()
    {
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Naziv,VrstaZivotinjePasminaId,SpolId,Boja,StatusId,DatumRodenja")] Zivotinje ljubimci, int vrstaZivotinjaId)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ljubimci);
            await _context.SaveChangesAsync();

            int subjektId = await DohvatiSubjektId();

            _context.SubjektiZivotinje.Add(new SubjektiZivotinje
            {
                SubjektId = subjektId,
                ZivotinjaId = ljubimci.Id
            });
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        await PripremiSifrarnike(vrstaZivotinjaId);
        ViewBag.OtvoriCreateModal = true;
        ViewBag.NoviLjubimac = ljubimci;

        return View(nameof(Index), await DohvatiLjubimce());
    }

    public async Task<IActionResult> Edit(int? id, string? tab = null)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ljubimci = await _context.Zivotinje
            .Include(l => l.ZivotinjeVrstePasmina)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (ljubimci == null)
        {
            return NotFound();
        }

        await PripremiSifrarnike(ljubimci.ZivotinjeVrstePasmina?.VrstaZivotinjeId);
        await PripremiSifrarnikeZdravstvenihZapisa();
        await PripremiPrehranu(ljubimci.Id);
        await PripremiZdravstveneZapise(ljubimci.Id);
        await PripremiIdentifikacijskeOznake(ljubimci);

        ViewBag.OtvoriOznakuTabId = tab;

        return View(ljubimci);
    }

    private async Task PripremiIdentifikacijskeOznake(Zivotinje ljubimac)
    {
        int? vrstaZivotinjeId = ljubimac.ZivotinjeVrstePasmina?.VrstaZivotinjeId;

        var vrsteOznaka = OznakePoVrstiZivotinje.DohvatiOznake(vrstaZivotinjeId);

        var oznake = await _context.IdentifikacijskeOznakeZivotinje
            .Where(o => o.ZivotinjaId == ljubimac.Id)
            .ToListAsync();

        var oznakeIds = oznake.Select(o => o.Id).ToList();

        var mikrocipi = await _context.DetaljiMikrocipaLjubimaca
            .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
            .ToListAsync();

        var prsteni = await _context.DetaljiPrstenaLjubimaca
            .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
            .ToListAsync();

        var tetovaze = await _context.DetaljiTetovazeLjubimaca
            .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
            .ToListAsync();

        var model = new List<IdentifikacijskaOznakaViewModel>();

        foreach (var vrsta in vrsteOznaka)
        {
            var oznaka = oznake
                .FirstOrDefault(o => o.VrstaIdentifikacijskeOznakeId == (int)vrsta);

            model.Add(new IdentifikacijskaOznakaViewModel
            {
                Vrsta = vrsta,
                Oznaka = oznaka,
                DetaljiMikrocipa = oznaka == null
                    ? null
                    : mikrocipi.FirstOrDefault(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id),
                DetaljiPrstena = oznaka == null
                    ? null
                    : prsteni.FirstOrDefault(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id),
                DetaljiTetovaze = oznaka == null
                    ? null
                    : tetovaze.FirstOrDefault(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id)
            });
        }

        ViewBag.IdentifikacijskeOznake = model;

        ViewBag.Drzave = new SelectList(
            await _context.Drzave
                .OrderBy(d => d.Naziv)
                .ToListAsync(),
            "Id", "Naziv");

        ViewBag.TipoviPrstena = new SelectList(
            await _context.TipPrstena
                .Where(t => t.Status)
                .OrderBy(t => t.Naziv)
                .ToListAsync(),
            "Id", "Naziv");
    }

    private async Task PripremiSifrarnikeZdravstvenihZapisa(int? vrstaZapisaId = null)
    {
        ViewBag.VrsteZdravstvenihZapisa = new SelectList(
            await _context.ZdravstveniZapisiVrste
                .Where(v => v.Status)
                .OrderBy(v => v.Naziv)
                .ToListAsync(),
            "Id", "Naziv", vrstaZapisaId);
    }

    private async Task PripremiPrehranu(int zivotinjaId)
    {
        ViewBag.Prehrana = await _context.Prehrana
            .Where(p => p.ZivotinjaId == zivotinjaId)
            .OrderBy(p => p.Naziv)
            .ToListAsync();
    }

    private async Task PripremiZdravstveneZapise(int zivotinjaId)
    {
        ViewBag.ZdravstveniZapisi = await _context.ZdravstveniZapisi
            .Where(z => z.ZivotinjaId == zivotinjaId)
            .Include(z => z.ZdravstveniZapisiVrste)
            .OrderByDescending(z => z.Datum)
            .ToListAsync();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Naziv,VrstaZivotinjePasminaId,SpolId,Boja,StatusId,DatumRodenja")] Zivotinje ljubimci, int vrstaZivotinjaId)
    {
        if (id != ljubimci.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ljubimci);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LjubimciExists(ljubimci.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        await PripremiSifrarnike(vrstaZivotinjaId);
        await PripremiSifrarnikeZdravstvenihZapisa();
        await PripremiPrehranu(ljubimci.Id);
        await PripremiZdravstveneZapise(ljubimci.Id);

        var postojeci = await _context.Zivotinje
            .AsNoTracking()
            .Include(l => l.ZivotinjeVrstePasmina)
            .FirstOrDefaultAsync(l => l.Id == ljubimci.Id);

        if (postojeci != null)
        {
            await PripremiIdentifikacijskeOznake(postojeci);
        }

        return View(ljubimci);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id)
    {
        var ljubimci = await _context.Zivotinje.FindAsync(id);

        if (ljubimci != null)
        {
            var veze = await _context.SubjektiZivotinje
                .Where(sl => sl.ZivotinjaId == ljubimci.Id)
                .ToListAsync();

            var prehrana = await _context.Prehrana
                .Where(p => p.ZivotinjaId == ljubimci.Id)
                .ToListAsync();

            var zdravstveniZapisi = await _context.ZdravstveniZapisi
                .Where(z => z.ZivotinjaId == ljubimci.Id)
                .ToListAsync();

            var oznake = await _context.IdentifikacijskeOznakeZivotinje
                .Where(o => o.ZivotinjaId == ljubimci.Id)
                .ToListAsync();

            var oznakeIds = oznake.Select(o => o.Id).ToList();

            _context.DetaljiMikrocipaLjubimaca.RemoveRange(
                await _context.DetaljiMikrocipaLjubimaca
                    .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
                    .ToListAsync());

            _context.DetaljiPrstenaLjubimaca.RemoveRange(
                await _context.DetaljiPrstenaLjubimaca
                    .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
                    .ToListAsync());

            _context.DetaljiTetovazeLjubimaca.RemoveRange(
                await _context.DetaljiTetovazeLjubimaca
                    .Where(d => oznakeIds.Contains(d.IdentifikacijskaOznakaZivotinjeId))
                    .ToListAsync());

            _context.IdentifikacijskeOznakeZivotinje.RemoveRange(oznake);
            _context.SubjektiZivotinje.RemoveRange(veze);
            _context.Prehrana.RemoveRange(prehrana);
            _context.ZdravstveniZapisi.RemoveRange(zdravstveniZapisi);
            _context.Zivotinje.Remove(ljubimci);

            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SpremiPrehranu([Bind("Id,ZivotinjaId,Naziv,VrstaHrane,NazivHrane,Proizvodac,KolicinaHrane,BrojObrokaDnevno,NamirniceZaIzbjegavanje,Napomena")] Prehrana prehrana)
    {
        if (!LjubimciExists(prehrana.ZivotinjaId))
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            prehrana.Status = true;

            if (prehrana.Id == 0)
            {
                _context.Prehrana.Add(prehrana);
            }
            else
            {
                _context.Prehrana.Update(prehrana);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = prehrana.ZivotinjaId });
        }

        var ljubimci = await _context.Zivotinje
            .Include(l => l.ZivotinjeVrstePasmina)
            .FirstOrDefaultAsync(l => l.Id == prehrana.ZivotinjaId);

        if (ljubimci == null)
        {
            return NotFound();
        }

        await PripremiSifrarnike(ljubimci.ZivotinjeVrstePasmina?.VrstaZivotinjeId);
        await PripremiSifrarnikeZdravstvenihZapisa();
        await PripremiPrehranu(ljubimci.Id);
        await PripremiZdravstveneZapise(ljubimci.Id);
        await PripremiIdentifikacijskeOznake(ljubimci);

        ViewBag.OtvoriPrehranaModal = true;
        ViewBag.NovaPrehrana = prehrana;

        return View(nameof(Edit), ljubimci);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ObrisiPrehranu(int? id)
    {
        var prehrana = await _context.Prehrana.FindAsync(id);

        if (prehrana == null)
        {
            return NotFound();
        }

        int zivotinjaId = prehrana.ZivotinjaId;

        _context.Prehrana.Remove(prehrana);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Edit), new { id = zivotinjaId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SpremiZdravstveniZapis([Bind("Id,ZivotinjaId,VrstaZdravstvenogZapisaId,Datum,Opis")] ZdravstveniZapisi zapis)
    {
        if (!LjubimciExists(zapis.ZivotinjaId))
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            zapis.Status = true;

            if (zapis.Id == 0)
            {
                _context.ZdravstveniZapisi.Add(zapis);
            }
            else
            {
                _context.ZdravstveniZapisi.Update(zapis);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = zapis.ZivotinjaId });
        }

        var ljubimci = await _context.Zivotinje
            .Include(l => l.ZivotinjeVrstePasmina)
            .FirstOrDefaultAsync(l => l.Id == zapis.ZivotinjaId);

        if (ljubimci == null)
        {
            return NotFound();
        }

        await PripremiSifrarnike(ljubimci.ZivotinjeVrstePasmina?.VrstaZivotinjeId);
        await PripremiSifrarnikeZdravstvenihZapisa(zapis.VrstaZdravstvenogZapisaId);
        await PripremiPrehranu(ljubimci.Id);
        await PripremiZdravstveneZapise(ljubimci.Id);
        await PripremiIdentifikacijskeOznake(ljubimci);

        ViewBag.OtvoriZdravstveniModal = true;
        ViewBag.NoviZdravstveniZapis = zapis;

        return View(nameof(Edit), ljubimci);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ObrisiZdravstveniZapis(int? id)
    {
        var zapis = await _context.ZdravstveniZapisi.FindAsync(id);

        if (zapis == null)
        {
            return NotFound();
        }

        int zivotinjaId = zapis.ZivotinjaId;

        _context.ZdravstveniZapisi.Remove(zapis);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Edit), new { id = zivotinjaId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SpremiIdentifikacijskuOznaku(
        [Bind("Id,ZivotinjaId,VrstaIdentifikacijskeOznakeId,Oznaka,DatumIzdavanja,DrzavaId,Oznacavatelj,Napomena")]
        IdentifikacijskeOznakeZivotinje unos,
        string? mjestoImplantacije,
        int? tipPrstenaId,
        string? noga,
        string? bojaPrstena,
        string? materijal,
        string? unutarnjiPromjer,
        int? godina,
        string? polozajTetovaze,
        string? bojaTetovaze,
        string? opisTetovaze)
    {
        var ljubimac = await _context.Zivotinje
            .Include(l => l.ZivotinjeVrstePasmina)
            .FirstOrDefaultAsync(l => l.Id == unos.ZivotinjaId);

        if (ljubimac == null)
        {
            return NotFound();
        }

        if (!OznakePoVrstiZivotinje.JeDozvoljena(
                ljubimac.ZivotinjeVrstePasmina?.VrstaZivotinjeId,
                unos.VrstaIdentifikacijskeOznakeId))
        {
            return BadRequest();
        }

        string idTaba = OznakePoVrstiZivotinje.IdTaba(
            (VrstaIdentifikacijskeOznake)unos.VrstaIdentifikacijskeOznakeId);

        if (!ModelState.IsValid)
        {
            await PripremiSifrarnike(ljubimac.ZivotinjeVrstePasmina?.VrstaZivotinjeId);
            await PripremiSifrarnikeZdravstvenihZapisa();
            await PripremiPrehranu(ljubimac.Id);
            await PripremiZdravstveneZapise(ljubimac.Id);
            await PripremiIdentifikacijskeOznake(ljubimac);

            ViewBag.OtvoriOznakuTabId = idTaba;

            return View(nameof(Edit), ljubimac);
        }

        if (unos.Id == 0)
        {
            _context.IdentifikacijskeOznakeZivotinje.Add(unos);
        }
        else
        {
            _context.IdentifikacijskeOznakeZivotinje.Update(unos);
        }

        await _context.SaveChangesAsync();

        await SpremiDetaljeOznake(
            unos,
            mjestoImplantacije,
            tipPrstenaId,
            noga,
            bojaPrstena,
            materijal,
            unutarnjiPromjer,
            godina,
            polozajTetovaze,
            bojaTetovaze,
            opisTetovaze);

        return RedirectToAction(nameof(Edit), new
        {
            id = unos.ZivotinjaId,
            tab = idTaba
        });
    }

    private async Task SpremiDetaljeOznake(
        IdentifikacijskeOznakeZivotinje oznaka,
        string? mjestoImplantacije,
        int? tipPrstenaId,
        string? noga,
        string? bojaPrstena,
        string? materijal,
        string? unutarnjiPromjer,
        int? godina,
        string? polozajTetovaze,
        string? bojaTetovaze,
        string? opisTetovaze)
    {
        switch ((VrstaIdentifikacijskeOznake)oznaka.VrstaIdentifikacijskeOznakeId)
        {
            case VrstaIdentifikacijskeOznake.Mikrocip:

                var mikrocip = await _context.DetaljiMikrocipaLjubimaca
                    .FirstOrDefaultAsync(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id);

                if (mikrocip == null)
                {
                    _context.DetaljiMikrocipaLjubimaca.Add(new DetaljiMikrocipaLjubimaca
                    {
                        IdentifikacijskaOznakaZivotinjeId = oznaka.Id,
                        MjestoImplantacije = mjestoImplantacije ?? string.Empty
                    });
                }
                else
                {
                    mikrocip.MjestoImplantacije = mjestoImplantacije ?? string.Empty;
                }

                break;

            case VrstaIdentifikacijskeOznake.Prsten:

                var prsten = await _context.DetaljiPrstenaLjubimaca
                    .FirstOrDefaultAsync(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id);

                if (prsten == null)
                {
                    _context.DetaljiPrstenaLjubimaca.Add(new DetaljiPrstenaLjubimaca
                    {
                        IdentifikacijskaOznakaZivotinjeId = oznaka.Id,
                        TipPrstenaId = tipPrstenaId ?? 0,
                        Noga = noga ?? string.Empty,
                        Boja = bojaPrstena ?? string.Empty,
                        Materijal = materijal ?? string.Empty,
                        UnutarnjiPromjer = unutarnjiPromjer ?? string.Empty,
                        Godina = godina ?? 0
                    });
                }
                else
                {
                    prsten.TipPrstenaId = tipPrstenaId ?? 0;
                    prsten.Noga = noga ?? string.Empty;
                    prsten.Boja = bojaPrstena ?? string.Empty;
                    prsten.Materijal = materijal ?? string.Empty;
                    prsten.UnutarnjiPromjer = unutarnjiPromjer ?? string.Empty;
                    prsten.Godina = godina ?? 0;
                }

                break;

            case VrstaIdentifikacijskeOznake.Tetovaza:

                var tetovaza = await _context.DetaljiTetovazeLjubimaca
                    .FirstOrDefaultAsync(d =>
                        d.IdentifikacijskaOznakaZivotinjeId == oznaka.Id);

                if (tetovaza == null)
                {
                    _context.DetaljiTetovazeLjubimaca.Add(new DetaljiTetovazeLjubimaca
                    {
                        IdentifikacijskaOznakaZivotinjeId = oznaka.Id,
                        PolozajTetovaze = polozajTetovaze ?? string.Empty,
                        Boja = bojaTetovaze ?? string.Empty,
                        Opis = opisTetovaze
                    });
                }
                else
                {
                    tetovaza.PolozajTetovaze = polozajTetovaze ?? string.Empty;
                    tetovaza.Boja = bojaTetovaze ?? string.Empty;
                    tetovaza.Opis = opisTetovaze;
                }

                break;
        }

        await _context.SaveChangesAsync();
    }

    private bool LjubimciExists(int? id)
    {
        return _context.Zivotinje.Any(e => e.Id == id);
    }
}
