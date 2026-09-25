using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZavrsniRad.Models;
using ZavrsniRad.Models.Enums;

namespace ZavrsniRad.Controllers
{
    [Authorize(Roles = RoleNazivi.AdministratorSustava)]
    public class AdministracijaController : Controller
    {
        private readonly PetTrackerDbContext _context;

        public AdministracijaController(PetTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PravniOblici()
        {
            return View(await DohvatiPravneOblike());
        }

        private async Task<List<Models.PravniOblici>> DohvatiPravneOblike()
        {
            return await _context.PravniOblici
                .OrderBy(p => p.Naziv)
                .ToListAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SpremiPravniOblik([Bind("Id,Naziv,Status")] Models.PravniOblici pravniOblik)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Podaci o pravnom obliku nisu ispravno uneseni";
                ViewBag.OtvoriModal = true;

                return View(nameof(PravniOblici), await DohvatiPravneOblike());
            }

            if (pravniOblik.Id == 0)
            {
                _context.PravniOblici.Add(pravniOblik);
            }
            else
            {
                var postojeci = await _context.PravniOblici.FindAsync(pravniOblik.Id);

                if (postojeci == null)
                {
                    return NotFound();
                }

                postojeci.Naziv = pravniOblik.Naziv;
                postojeci.Status = pravniOblik.Status;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PravniOblici));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ObrisiPravniOblik(int? id)
        {
            var pravniOblik = await _context.PravniOblici.FindAsync(id);

            if (pravniOblik == null)
            {
                return NotFound();
            }

            bool uUporabi = await _context.SubjektiDetalji
                .AnyAsync(d => d.PravniOblikId == pravniOblik.Id);

            if (uUporabi)
            {
                TempData["GreskaBrisanja"] =
                    $"Pravni oblik \"{pravniOblik.Naziv}\" nije moguÄ‡e obrisati jer je dodijeljen subjektima.";

                return RedirectToAction(nameof(PravniOblici));
            }

            _context.PravniOblici.Remove(pravniOblik);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PravniOblici));
        }

        public async Task<IActionResult> Djelatnosti()
        {
            return View(await DohvatiDjelatnosti());
        }

        private async Task<List<Models.Djelatnosti>> DohvatiDjelatnosti()
        {
            return await _context.Djelatnosti
                .OrderBy(d => d.Oznaka)
                .ToListAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SpremiDjelatnost([Bind("Id,Oznaka,Naziv,Status")] Models.Djelatnosti djelatnost)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Greska = "Podaci o djelatnosti nisu ispravno uneseni";
                ViewBag.OtvoriModal = true;

                return View(nameof(Djelatnosti), await DohvatiDjelatnosti());
            }

            if (djelatnost.Id == 0)
            {
                _context.Djelatnosti.Add(djelatnost);
            }
            else
            {
                var postojeca = await _context.Djelatnosti.FindAsync(djelatnost.Id);

                if (postojeca == null)
                {
                    return NotFound();
                }

                postojeca.Oznaka = djelatnost.Oznaka;
                postojeca.Naziv = djelatnost.Naziv;
                postojeca.Status = djelatnost.Status;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Djelatnosti));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ObrisiDjelatnost(int? id)
        {
            var djelatnost = await _context.Djelatnosti.FindAsync(id);

            if (djelatnost == null)
            {
                return NotFound();
            }

            bool uUporabi = await _context.SubjektiDetalji
                .AnyAsync(d => d.DjelatnostId == djelatnost.Id);

            if (uUporabi)
            {
                TempData["GreskaBrisanja"] =
                    $"Djelatnost \"{djelatnost.Naziv}\" nije moguÄ‡e obrisati jer je dodijeljena subjektima.";

                return RedirectToAction(nameof(Djelatnosti));
            }

            _context.Djelatnosti.Remove(djelatnost);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Djelatnosti));
        }
    }
}
