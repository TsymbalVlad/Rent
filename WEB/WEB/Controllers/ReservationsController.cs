using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Entity;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservationsController : Controller
    {
        private readonly AppDBContext _context;

        public ReservationsController(AppDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
              return _context.Reservation != null ? 
                          View(await _context.Reservation.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Reservation'  is null.");
        }

       

        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.status_id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        

        public IActionResult Create()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            try
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(reservation);
            }
            
        }

        

        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, Reservation reservation)
        {
            if (id != reservation.status_id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.status_id))
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
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.status_id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'AppDBContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(byte id)
        {
          return (_context.Reservation?.Any(e => e.status_id == id)).GetValueOrDefault();
        }
    }
}
