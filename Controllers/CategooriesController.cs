using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class CategooriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategooriesController(ModelContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categoories
        public async Task<IActionResult> Index()
        {
              return _context.Categoories != null ? 
                          View(await _context.Categoories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Categoories'  is null.");
        }

        // GET: Categoories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoory == null)
            {
                return NotFound();
            }

            return View(categoory);
        }

        // GET: Categoories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,ImageFile")] Categoory categoory)
        {
            if (ModelState.IsValid)
            {
                if (categoory.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" +
                    categoory.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/images/",
                    fileName);
                    using (var fileStream = new FileStream(path,
                    FileMode.Create))
                    {
                        await categoory.ImageFile.CopyToAsync(fileStream);
                    }
                    categoory.ImagePath = fileName;
                }
                _context.Add(categoory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoory);
        }

        // GET: Categoories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories.FindAsync(id);
            if (categoory == null)
            {
                return NotFound();
            }
            return View(categoory);
        }

        // POST: Categoories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,ImagePath,ImageFile")] Categoory categoory)
        {
            if (id != categoory.Id)
            {
                
                return NotFound();
            }
            if (categoory.ImageFile == null && string.IsNullOrEmpty(categoory.ImagePath))
            {
                ModelState.AddModelError("ImageFile", "An image file is required.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (categoory.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoory.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath, "images", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await categoory.ImageFile.CopyToAsync(fileStream);
                        }


                        if (!string.IsNullOrEmpty(categoory.ImagePath))
                        {
                            string oldPath = Path.Combine(wwwRootPath, "images", categoory.ImagePath);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }

                        categoory.ImagePath = fileName; 
                    }
                    else
                    {
                        _context.Entry(categoory).Property(x => x.ImagePath).IsModified = false;
                    }

                    _context.Update(categoory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategooryExists(categoory.Id))
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
            return View(categoory);
        }

        // GET: Categoories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoory == null)
            {
                return NotFound();
            }

            return View(categoory);
        }

        // POST: Categoories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Categoories == null)
            {
                return Problem("Entity set 'ModelContext.Categoories'  is null.");
            }
            var categoory = await _context.Categoories.FindAsync(id);
            if (categoory != null)
            {
                _context.Categoories.Remove(categoory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategooryExists(decimal id)
        {
          return (_context.Categoories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
