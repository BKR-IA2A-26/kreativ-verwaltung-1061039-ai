using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class SessionLogController : Controller
    {
        private readonly DmstContext _context;

        public SessionLogController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = _context.Sessionlogs.Include(s => s.Campaign);
            return View(await sessions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var session = await _context.Sessionlogs
                .Include(s => s.Campaign)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound();
            return View(session);
        }

        public IActionResult Create()
        {
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sessionlog session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(session);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var session = await _context.Sessionlogs.FindAsync(id);
            if (session == null) return NotFound();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sessionlog session)
        {
            if (id != session.SessionId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(session);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var session = await _context.Sessionlogs
                .Include(s => s.Campaign)
                .FirstOrDefaultAsync(s => s.SessionId == id);
            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessionlogs.FindAsync(id);
            if (session != null) _context.Sessionlogs.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}