using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class CampaignController : Controller
    {
        private readonly DmstContext _context;

        public CampaignController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Campaigns.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null) return NotFound();
            return View(campaign);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null) return NotFound();
            return View(campaign);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Campaign campaign)
        {
            if (id != campaign.CampaignId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null) return NotFound();
            return View(campaign);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign != null) _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}