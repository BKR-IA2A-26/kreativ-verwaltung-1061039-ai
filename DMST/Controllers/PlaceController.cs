using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class PlaceController : Controller
    {
        private readonly DmstContext _context;

        public PlaceController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var places = _context.Places.Include(p => p.Campaign);
            return View(await places.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var place = await _context.Places
                .Include(p => p.Campaign)
                .FirstOrDefaultAsync(p => p.PlaceId == id);
            if (place == null) return NotFound();
            return View(place);
        }

        public IActionResult Create()
        {
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(place);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var place = await _context.Places.FindAsync(id);
            if (place == null) return NotFound();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Place place)
        {
            if (id != place.PlaceId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View(place);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var place = await _context.Places
                .Include(p => p.Campaign)
                .FirstOrDefaultAsync(p => p.PlaceId == id);
            if (place == null) return NotFound();
            return View(place);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place != null) _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}