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
                return NotFound();

            return View(booking);
        }

        // ?? No antiforgery validation
[IgnoreAntiforgeryToken]
[HttpPost]
public IActionResult Create(Booking booking)
{
    System.Console.WriteLine("?? POST HIT");

    if (!ModelState.IsValid)
    {
        System.Console.WriteLine("? ModelState invalid");
        foreach (var e in ModelState)
        {
            System.Console.WriteLine($"{e.Key} = {string.Join(",", e.Value.Errors.Select(er => er.ErrorMessage))}");
        }

        return Content("MODEL INVALID");
    }

    return Content("MODEL VALID");
}    }
}
