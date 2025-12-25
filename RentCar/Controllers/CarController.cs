using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;

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
