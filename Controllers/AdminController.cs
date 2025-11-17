using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CarCleanz.Data;
using System.Linq;

namespace CarCleanz.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var bookings = _context.Bookings.ToList();
            return View(bookings);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please fill all fields.";
                return View();
            }

            var admin = _context.AdminViews
                .FirstOrDefault(a =>
                    a.Username.ToLower().Trim() == username.ToLower().Trim() &&
                    a.Password.Trim() == password.Trim()
                );

            if (admin != null)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }
    }
}