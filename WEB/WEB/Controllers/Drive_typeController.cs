using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Entity;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Drive_typeController : Controller
    {
        private readonly AppDBContext _context;

        public Drive_typeController(AppDBContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
              return _context.Drive_type != null ? 
                          View(await _context.Drive_type.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Drive_type'  is null.");
        }



        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Drive_type == null)
            {
                return NotFound();
            }

            var drive_type = await _context.Drive_type
                .FirstOrDefaultAsync(m => m.drive_id == id);
            if (drive_type == null)
            {
                return NotFound();
            }

            return View(drive_type);
        }



        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drive_type drive_type)
        {
            try
            {
                _context.Add(drive_type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(drive_type);
            }
        }



        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Drive_type == null)
            {
                return NotFound();
            }

            var drive_type = await _context.Drive_type.FindAsync(id);
            if (drive_type == null)
            {
                return NotFound();
            }
            return View(drive_type);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, Drive_type drive_type)
        {
            if (id != drive_type.drive_id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(drive_type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Drive_typeExists(drive_type.drive_id))
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



        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Drive_type == null)
            {
                return NotFound();
            }

            var drive_type = await _context.Drive_type
                .FirstOrDefaultAsync(m => m.drive_id == id);
            if (drive_type == null)
            {
                return NotFound();
            }

            return View(drive_type);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Drive_type == null)
            {
                return Problem("Entity set 'AppDBContext.Drive_type'  is null.");
            }
            var drive_type = await _context.Drive_type.FindAsync(id);
            if (drive_type != null)
            {
                _context.Drive_type.Remove(drive_type);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Drive_typeExists(byte id)
        {
          return (_context.Drive_type?.Any(e => e.drive_id == id)).GetValueOrDefault();
        }
    }
}
