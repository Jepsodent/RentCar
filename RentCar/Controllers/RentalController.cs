using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Models;
using System.Security.Claims;

namespace RentCar.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly AppDbContext _context;

        public RentalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(string carId, DateTime pickupDate, DateTime returnDate)
        {

            if (returnDate < pickupDate)
            {
                TempData["ErrorMessage"] = "Tanggal kembali tidak boleh lebih kecil dari tanggal ambil.";
                return RedirectToAction("Detail", "Car", new { id = carId, pickupDate, returnDate });
            }

            var car = await _context.MsCars.FindAsync(carId);
            if (car == null)
            {
                return NotFound();
            }

            var totalDays = (returnDate - pickupDate).Days;
            if (totalDays < 1)
            {
                totalDays = 1;
            }
            var totalPrice = totalDays * car.PricePerDay;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Auth");

            var rental = new TrRental
            {
                RentalId = Guid.NewGuid().ToString(),
                CarId = carId,
                CustomerId = userId,
                RentalDate = pickupDate,
                ReturnDate = returnDate,
                TotalPrice = totalPrice,
                PaymentStatus = false
            };

            _context.TrRentals.Add(rental);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Car booked successfully! Please proceed to payment.";
            return RedirectToAction("History");

        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var historyList = await _context.TrRentals
                                            .Include(r => r.Car)
                                            .Where(r => r.CustomerId == userId)
                                            .OrderByDescending(r => r.RentalDate)
                                            .ToListAsync();
            return View(historyList);
        } 



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(string rentalId) 
        {
            var rental = await _context.TrRentals.FindAsync(rentalId);
            if (rental == null)
            {
                return NotFound();
            }
            rental.PaymentStatus = true;
            _context.TrRentals.Update(rental);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Payment successful!";
            return RedirectToAction("History");
        }
    }
}
