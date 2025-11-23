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
    if (!ModelState.IsValid)
    {
        return View(booking);
    }

    // **Prevent selecting past date**
    if (booking.BookingDate.HasValue && booking.BookingDate.Value.Date < DateTime.UtcNow.Date)
    {
        ModelState.AddModelError("BookingDate", "Booking date cannot be a past date.");
        return View(booking);
    }

    // **Force booking date to TODAY if user selected older date (extra safety)**
    booking.BookingDate = DateTime.UtcNow;

    // **Generate Custom Booking ID**
    booking.CustomBookingId = "CC" + DateTime.UtcNow.Ticks;

    // **Assign Price based on vehicle type**
    booking.Price = booking.VehicleType.ToLower() switch
    {
        "hatchback" => 499,
        "sedan"     => 599,
        "suv"       => 699,
        _           => 0
    };

    // **Save to DB**
    _context.Bookings.Add(booking);
    _context.SaveChanges();

    // **Redirect to Payment page**
    return RedirectToAction("Payment", new { id = booking.Id });
}
    }
}
