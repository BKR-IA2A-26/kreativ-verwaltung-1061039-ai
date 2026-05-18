using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class QuestController : Controller
    {
        private readonly DmstContext _context;

        public QuestController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var quests = _context.Quests.Include(q => q.Campaign);
            return View(await quests.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var quest = await _context.Quests
                .Include(q => q.Campaign)
                .FirstOrDefaultAsync(q => q.QuestId == id);
            if (quest == null) return NotFound();
            return View(quest);
        }

        public IActionResult Create()
        {
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quest quest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(quest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null) return NotFound();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(quest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Quest quest)
        {
            if (id != quest.QuestId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(quest);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var quest = await _context.Quests
                .Include(q => q.Campaign)
                .FirstOrDefaultAsync(q => q.QuestId == id);
            if (quest == null) return NotFound();
            return View(quest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest != null) _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}