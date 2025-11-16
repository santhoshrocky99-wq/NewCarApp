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
            // Fix ModelState for optional date
            ModelState.Remove("BookingDate");

            if (!ModelState.IsValid)
            {
                return Content("MODEL INVALID");
            }

            // Auto booking date
            booking.BookingDate ??= DateTime.Now;

            // Auto-generate custom booking id
            var lastBooking = _context.Bookings
                .OrderByDescending(b => b.Id)
                .FirstOrDefault();

            int nextNumber = 3000;

            if (lastBooking != null && !string.IsNullOrEmpty(lastBooking.CustomBookingId))
            {
                string numberPart = lastBooking.CustomBookingId.Replace("CCA", "");
                int.TryParse(numberPart, out nextNumber);
                nextNumber++;
            }

            booking.CustomBookingId = $"CCA{nextNumber}";

            // Pricing logic
            switch ((booking.VehicleType ?? "").ToLower())
            {
                case "hatchback": booking.Price = 499; break;
                case "sedan": booking.Price = 650; break;
                case "suv": booking.Price = 750; break;
                default: booking.Price = 0; break;
            }

            // Save to database
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            // Redirect to payment page
            return RedirectToAction("Payment", new { id = booking.Id });
        }
    }
}