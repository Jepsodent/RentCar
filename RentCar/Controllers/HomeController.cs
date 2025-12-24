using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(DateTime? pickupDate, DateTime? returnDate, int? yearFilter)
        {

            if (pickupDate.HasValue && returnDate.HasValue)
            {
                if (returnDate < pickupDate)
                {
                    ViewBag.ErrorMessage = "Tanggal pengembalian tidak boleh kurang dari tanggal pengambilan!";

                    var allCars = await _context.MsCars.Include(c => c.CarImages).ToListAsync();
                    return View(allCars);
                }
            }

            var carsQuery = _context.MsCars
                                    .Include(c => c.CarImages)
                                    .AsQueryable();

            if (pickupDate.HasValue && returnDate.HasValue)
            {
                
                var bookedCarIds = _context.TrRentals
                    .Where(r => r.RentalDate <= returnDate && r.ReturnDate >= pickupDate)
                    .Select(r => r.CarId); 

                carsQuery = carsQuery.Where(c => !bookedCarIds.Contains(c.CarId));
            }

            if (yearFilter.HasValue && yearFilter.Value > 0)
            {
                carsQuery = carsQuery.Where(c => c.Year == yearFilter.Value);
            }

            var carList = await carsQuery.ToListAsync();
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;
            ViewBag.YearFilter = yearFilter;

            return View(carList);
        }

 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
