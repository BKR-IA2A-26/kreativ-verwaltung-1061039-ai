using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class PlayercharakterController : Controller
    {
        private readonly DmstContext _context;

        public PlayercharakterController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chars = _context.Playercharakters.Include(p => p.Campaign);
            return View(await chars.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pc = await _context.Playercharakters
                .Include(p => p.Campaign)
                .FirstOrDefaultAsync(p => p.CharId == id);
            if (pc == null) return NotFound();
            return View(pc);
        }

        public IActionResult Create()
        {
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Playercharakter pc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(pc);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pc = await _context.Playercharakters.FindAsync(id);
            if (pc == null) return NotFound();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(pc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Playercharakter pc)
        {
            if (id != pc.CharId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(pc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(pc);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pc = await _context.Playercharakters
                .Include(p => p.Campaign)
                .FirstOrDefaultAsync(p => p.CharId == id);
            if (pc == null) return NotFound();
            return View(pc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pc = await _context.Playercharakters.FindAsync(id);
            if (pc != null) _context.Playercharakters.Remove(pc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}