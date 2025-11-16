using Microsoft.AspNetCore.Mvc;
using CarCleanz.Data;
using CarCleanz.Models;
using System;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Payment(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
[HttpPost]
public IActionResult Create(Booking booking)
{
    ModelState.Remove("BookingDate");  // Remove validation for BookingDate

    if (!ModelState.IsValid)
    {
        return View(booking);
    }

    booking.BookingDate = DateTime.UtcNow;  // Auto assign here

    var lastBooking = _context.Bookings
        .OrderByDescending(b => b.Id)
        .FirstOrDefault();

    int nextNumber = 3000;

    if (lastBooking != null && !string.IsNullOrEmpty(lastBooking.CustomBookingId))
    {
        string numberPart = lastBooking.CustomBookingId.Replace("CCA", "");
        nextNumber = int.Parse(numberPart) + 1;
    }

    booking.CustomBookingId = $"CCA{nextNumber}";

    switch ((booking.VehicleType ?? "").ToLower())
    {
        case "hatchback": booking.Price = 499; break;
        case "sedan":     booking.Price = 650; break;
        case "suv":       booking.Price = 750; break;
        default:          booking.Price = 0;   break;
    }

    _context.Bookings.Add(booking);
    _context.SaveChanges();

    return RedirectToAction("Payment", new { id = booking.Id });
}



    }
}
