using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Entity;
using WEB.Models;

namespace WEB.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDBContext _context;

        public CarsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
              return _context.Car != null ? 
                          View(await _context.Car.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Car'  is null.");
        }

        public async Task<IEnumerable<Fuel>> GetFuel()
        {
            var fuel = _context.Fuels.ToList();
            return fuel;
        }

       public async Task<IEnumerable<CarType>> GetCarType()
        {
            var cartype = _context.CarType.ToList();
            return cartype;
        }

        public async Task<IEnumerable<Transmission>> GetTransmission()
        {
            var transmission = _context.Transmission.ToList();
            return transmission;
        }

        public async Task<IEnumerable<Reservation>> GetReservationStatus()
        {
            var res = _context.Reservation.ToList();
            return res;
        }

        public async Task<IEnumerable<Drive_type>> GetDriveType()
        {
            var drive = _context.Drive_type.ToList();
            return drive;
        }

        public async Task<IEnumerable<BodyType>> GetCarBody()
        {
            var body = _context.BodyType.ToList();
            return body;
        }



        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.car_id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }



        public IActionResult Create()
        {
            var model = new NewCar
            {
                Fuels = GetFuel().Result.ToList(),
                CarType = GetCarType().Result.ToList(),
                Transmission = GetTransmission().Result.ToList(),
                Reservation = GetReservationStatus().Result.ToList(),
                Drive_type = GetDriveType().Result.ToList(),
                BodyType = GetCarBody().Result.ToList()
            };
            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            try
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(car);
            }
        }



        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, Car car)
        {
            if (id != car.car_id)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.car_id))
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



        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.car_id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'AppDBContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(short id)
        {
          return (_context.Car?.Any(e => e.car_id == id)).GetValueOrDefault();
        }
    }
}
