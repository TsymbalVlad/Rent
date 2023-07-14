using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Entity;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarTypesController : Controller
    {
        private readonly AppDBContext _context;

        public CarTypesController(AppDBContext context)
        {
            _context = context;
        }

        

        public async Task<IActionResult> Index()
        {
              return _context.CarType != null ? 
                          View(await _context.CarType.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.CarType'  is null.");
        }



        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType
                .FirstOrDefaultAsync(m => m.type_id == id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }



        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarType carType)
        {
            try
            {
                _context.Add(carType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(carType);
            }
        }



        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }
            return View(carType);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, CarType carType)
        {
            if (id != carType.type_id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(carType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarTypeExists(carType.type_id))
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
            if (id == null || _context.CarType == null)
            {
                return NotFound();
            }

            var carType = await _context.CarType
                .FirstOrDefaultAsync(m => m.type_id == id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.CarType == null)
            {
                return Problem("Entity set 'AppDBContext.CarType'  is null.");
            }
            var carType = await _context.CarType.FindAsync(id);
            if (carType != null)
            {
                _context.CarType.Remove(carType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarTypeExists(byte id)
        {
          return (_context.CarType?.Any(e => e.type_id == id)).GetValueOrDefault();
        }
    }
}
