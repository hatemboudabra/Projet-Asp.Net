#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliothequeVieuxMontreal.Models;

namespace BibliothequeVieuxMontreal.Controllers
{
    public class PretsController : Controller
    {
        private readonly biblioContext _context;

        public PretsController(biblioContext context)
        {
            _context = context;
        }

        // GET: Prets
        public async Task<IActionResult> Index()
        {
            var biblioContext = _context.Prets.Include(p => p.IdLivreNavigation).Include(p => p.IdMembreNavigation);
            return View(await biblioContext.ToListAsync());
        }

        // GET: Prets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pret = await _context.Prets
                .Include(p => p.IdLivreNavigation)
                .Include(p => p.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pret == null)
            {
                return NotFound();
            }

            return View(pret);
        }

        // GET: Prets/Create
        public IActionResult Create()
        {
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre");
            ViewData["IdMembre"] = new SelectList(_context.Membres, "Id", "Nom");
            return View();
        }

        // POST: Prets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdLivre,IdMembre,DateDebut")] Pret pret)
        {
            pret.DateDebut = DateTime.Now;
            pret.DateFin = pret.DateDebut.AddDays(7);
            _context.Add(pret);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Prets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pret = await _context.Prets.FindAsync(id);
            if (pret == null)
            {
                return NotFound();
            }
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre", pret.IdLivre);
            ViewData["IdMembre"] = new SelectList(_context.Membres, "Id", "Nom", pret.IdMembre);
            return View(pret);
        }

        // POST: Prets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdLivre,IdMembre,DateDebut,DateFin")] Pret pret)
        {
            if (id != pret.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pret);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PretExists(pret.Id))
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
            ViewData["IdLivre"] = new SelectList(_context.Livres, "Id", "Titre", pret.IdLivre);
            ViewData["IdMembre"] = new SelectList(_context.Membres, "Id", "Nom", pret.IdMembre);
            return View(pret);
        }

        // GET: Prets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pret = await _context.Prets
                .Include(p => p.IdLivreNavigation)
                .Include(p => p.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pret == null)
            {
                return NotFound();
            }

            return View(pret);
        }

        // POST: Prets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pret = await _context.Prets.FindAsync(id);
            ViewBag.pret = pret;
            if (DateTime.Today.Date > pret.DateFin)
            {
                ViewBag.message = "❌ Vous êtes en retard ! 😠";
                ViewBag.typeMessage = "danger";
                // Insertion dans la table retard :
                var retard = new Retard();
                retard.IdMembre = pret.IdMembre;
                retard.IdLivre = pret.IdLivre;
                retard.DatePret = pret.DateDebut;
                retard.Nbjour = (int)DateTime.Today.Date.Subtract(pret.DateFin).TotalDays;
                _context.Add(retard);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.message = "✔ Livre rendu dans les délais ! Merci 😊";
                ViewBag.typeMessage = "success";
            }
            _context.Prets.Remove(pret);
            await _context.SaveChangesAsync();
            ViewBag.idMembre = pret.IdMembre;
            return View(pret);
        }

        private bool PretExists(int id)
        {
            return _context.Prets.Any(e => e.Id == id);
        }
    }
}
