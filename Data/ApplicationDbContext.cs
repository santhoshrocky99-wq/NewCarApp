using Microsoft.EntityFrameworkCore;
using CarCleanz.Models;

namespace CarCleanz.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AdminView> AdminViews { get; set; }
    }
}