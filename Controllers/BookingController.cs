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
        // ALWAYS force UTC for PostgreSQL
        booking.BookingDate = DateTime.UtcNow;

        // OR if you're allowing user-specified date:
        // if (booking.BookingDate.HasValue)
        //     booking.BookingDate = DateTime.SpecifyKind(booking.BookingDate.Value, DateTimeKind.Utc);
        // else
        //     booking.BookingDate = DateTime.UtcNow;

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        TempData["Success"] = "Booking saved successfully!";
        return RedirectToAction("Index", "Home");
    }

    return View(booking);
}
    }
}