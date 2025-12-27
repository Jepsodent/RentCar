using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using System.Diagnostics;

namespace RentCar.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly AppDbContext _context;

        public CarController(AppDbContext context)
        {
            _context = context;
        }


         public async Task<IActionResult> Index(DateTime? pickupDate, DateTime? returnDate, int? yearFilter, string sortOrder, int page = 1)
        {

            if (!pickupDate.HasValue && !returnDate.HasValue && !yearFilter.HasValue)
            {
                return View(new List<MsCar>());
            }


            if (pickupDate.HasValue && returnDate.HasValue)
            {
                if (returnDate < pickupDate)
                {
                    ViewBag.ErrorMessage = "Tanggal pengembalian tidak boleh kurang dari tanggal pengambilan!";

                    return View(new List<MsCar>());
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
            switch (sortOrder)
            {
                case "price_desc": // Termahal -> Termurah
                    carsQuery = carsQuery.OrderByDescending(c => c.PricePerDay);
                    break;
                case "price_asc": // Termurah -> Termahal
                default:
                    carsQuery = carsQuery.OrderBy(c => c.PricePerDay);
                    break;
            }
            int pageSize = 3;

            int totalItems = await carsQuery.CountAsync();
            Console.WriteLine("Total Items: " + totalItems);
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var carList = await carsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;
            ViewBag.YearFilter = yearFilter;
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(carList);
         }

        [HttpGet("Car/{id}")]
        public async Task<IActionResult> Detail(string id, DateTime? pickupDate, DateTime? returnDate)
        {
            if (id == null) return NotFound();
            var car = await _context.MsCars.Include(c => c.CarImages)
                                          .FirstOrDefaultAsync(c => c.CarId == id);
            if (car == null) return NotFound();
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;

            return View(car);
        }
        
    }
}
