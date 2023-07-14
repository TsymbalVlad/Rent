using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Controllers
{

    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cars = await _context.Set<Cars>().FromSqlRaw("SELECT Top 3 * FROM CarView Order by price Desc").ToListAsync();
                return View(cars);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Cars()
        {
            try { 
            var cars = await _context.Set<Cars>().FromSqlRaw("SELECT * FROM CarView").ToListAsync();
            return View(cars);
            }
            catch (Exception ex)
            {
                return View(Index);
            }
        }
    }
}