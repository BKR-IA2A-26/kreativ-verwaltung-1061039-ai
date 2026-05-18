using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class NpcController : Controller
    {
        private readonly DmstContext _context;

        public NpcController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var npcs = _context.Npcs.Include(n => n.Campaign).Include(n => n.Place);
            return View(await npcs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var npc = await _context.Npcs
                .Include(n => n.Campaign)
                .Include(n => n.Place)
                .FirstOrDefaultAsync(n => n.NpcId == id);
            if (npc == null) return NotFound();
            return View(npc);
        }

        public IActionResult Create()
        {
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Places = _context.Places.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Npc npc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(npc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Places = _context.Places.ToList();
            return View(npc);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var npc = await _context.Npcs.FindAsync(id);
            if (npc == null) return NotFound();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Places = _context.Places.ToList();
            return View(npc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Npc npc)
        {
            if (id != npc.NpcId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(npc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Places = _context.Places.ToList();
            return View(npc);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var npc = await _context.Npcs
                .Include(n => n.Campaign)
                .Include(n => n.Place)
                .FirstOrDefaultAsync(n => n.NpcId == id);
            if (npc == null) return NotFound();
            return View(npc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var npc = await _context.Npcs.FindAsync(id);
            if (npc != null) _context.Npcs.Remove(npc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}