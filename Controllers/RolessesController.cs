using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class RolessesController : Controller
    {
        private readonly ModelContext _context;

        public RolessesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rolesses
        public async Task<IActionResult> Index()
        {
              return _context.Rolesses != null ? 
                          View(await _context.Rolesses.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Rolesses'  is null.");
        }

        // GET: Rolesses/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rolesses == null)
            {
                return NotFound();
            }

            var roless = await _context.Rolesses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roless == null)
            {
                return NotFound();
            }

            return View(roless);
        }

        // GET: Rolesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rolesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rolename")] Roless roless)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roless);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roless);
        }

        // GET: Rolesses/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rolesses == null)
            {
                return NotFound();
            }

            var roless = await _context.Rolesses.FindAsync(id);
            if (roless == null)
            {
                return NotFound();
            }
            return View(roless);
        }

        // POST: Rolesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Rolename")] Roless roless)
        {
            if (id != roless.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roless);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolessExists(roless.Id))
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
            return View(roless);
        }

        // GET: Rolesses/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rolesses == null)
            {
                return NotFound();
            }

            var roless = await _context.Rolesses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roless == null)
            {
                return NotFound();
            }

            return View(roless);
        }

        // POST: Rolesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rolesses == null)
            {
                return Problem("Entity set 'ModelContext.Rolesses'  is null.");
            }
            var roless = await _context.Rolesses.FindAsync(id);
            if (roless != null)
            {
                _context.Rolesses.Remove(roless);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolessExists(decimal id)
        {
          return (_context.Rolesses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
