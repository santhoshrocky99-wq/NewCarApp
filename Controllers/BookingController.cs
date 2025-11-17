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
        [IgnoreAntiforgeryToken]
[HttpPost]
public IActionResult Create(Booking booking)
{
    if (ModelState.IsValid)
    {
        // Fix DateTime for PostgreSQL
        if (booking.BookingDate.HasValue)
            booking.BookingDate = DateTime.SpecifyKind(booking.BookingDate.Value, DateTimeKind.Utc);
        else
            booking.BookingDate = DateTime.UtcNow;

        // Auto price logic
        booking.Price = booking.VehicleType.ToLower() switch
        {
            "hatchback" => 499,
            "sedan" => 650,
            "suv" => 750,
            _ => 0
        };

        // Generate custom ID
        booking.CustomBookingId = "CB" + DateTime.UtcNow.Ticks;

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        // Redirect to Success page with ID
        return RedirectToAction("Success", new { id = booking.Id });
    }

    return View(booking);
}
    }
}