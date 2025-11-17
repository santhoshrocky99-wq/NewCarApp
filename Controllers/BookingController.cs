using Microsoft.AspNetCore.Mvc;
using CarCleanz.Data;
using CarCleanz.Models;
using System.Linq;

namespace CarCleanz.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Booking/Payment/5
        [HttpGet]
        public IActionResult Payment(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }
[HttpGet]
public IActionResult Success(int id)
{
    var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);

    if (booking == null)
        return NotFound();

    return View(booking);
}

        // POST: Booking/Create
        [HttpPost]
[IgnoreAntiforgeryToken]
public IActionResult Create(Booking booking)
{
    if (ModelState.IsValid)
    {
        // 1) Generate Custom Booking ID
        int nextId = _context.Bookings.Count() + 1;
        booking.CustomBookingId = $"CCZ-{nextId:D5}";

        // 2) Set Price (simple example)
        booking.Price = booking.VehicleType.ToLower() switch
        {
            "sedan" => 499,
            "suv" => 599,
            "hatchback" => 399,
            _ => 0
        };

        // 3) Fix DateTime for PostgreSQL
        booking.BookingDate = DateTime.UtcNow;

        // 4) Save to DB
        _context.Bookings.Add(booking);
        _context.SaveChanges();

        TempData["Success"] = "Booking saved successfully!";
        return RedirectToAction("Index", "Home");
    }

    return View(booking);
}
    }
}