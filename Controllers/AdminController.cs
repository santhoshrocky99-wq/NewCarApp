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
            // ? Security Check — Only allow logged-in admin
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            // ? Fetch all bookings from database
            var bookings = _context.Bookings.ToList();

            // ? Pass them to the View
            return View(bookings);
        }

        // Admin Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "1234")
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