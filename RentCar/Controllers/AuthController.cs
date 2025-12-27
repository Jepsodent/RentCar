using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Filters;
using RentCar.Models;
using RentCar.ViewModel.Auth;
using System.Security.Claims;

namespace RentCar.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        [RedirectIfAuthenticate]

        [HttpGet]
        public IActionResult Register()
        {
           
            return View();
        }

        [RedirectIfAuthenticate]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel customer)
        {
            if(await _context.MsCustomers.AnyAsync(c => c.Email == customer.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(customer);
            }

            if (ModelState.IsValid)
            {
                var newCustomer = new MsCustomer
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    Name = customer.Name,
                    Email = customer.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(customer.Password),
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    DriverLicenseNumber = customer.DriverLicenseNumber
                };
                _context.MsCustomers.Add(newCustomer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Registrasi successfull! Please login.";
                return RedirectToAction("Login");
            }
            return View(customer);
        }

        [RedirectIfAuthenticate]
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [RedirectIfAuthenticate]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var user = await _context.MsCustomers
                .FirstOrDefaultAsync(c => c.Email == email);

            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.CustomerId.ToString()),
                    new Claim(ClaimTypes.Role, "Customer")
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync("Cookies", principal, authProperties);

                return RedirectToAction("Index", "Car");
            }
            ViewBag.ErrorMessage = "Email atau Password salah!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index","Home");
        }

    }
}
