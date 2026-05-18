using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DMST.Data;
using DMST.Models;

namespace DMST.Controllers
{
    public class ItemLootController : Controller
    {
        private readonly DmstContext _context;

        public ItemLootController(DmstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = _context.ItemLoots.Include(i => i.Place);
            return View(await items.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.ItemLoots
                .Include(i => i.Place)
                .FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Places = _context.Places.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemLoot item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Places = _context.Places.ToList();
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.ItemLoots.FindAsync(id);
            if (item == null) return NotFound();
            ViewBag.Places = _context.Places.ToList();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemLoot item)
        {
            if (id != item.ItemId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Places = _context.Places.ToList();
            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.ItemLoots
                .Include(i => i.Place)
                .FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ItemLoots.FindAsync(id);
            if (item != null) _context.ItemLoots.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}