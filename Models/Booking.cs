using System;
using System.ComponentModel.DataAnnotations;

namespace CarCleanz.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public string? CustomBookingId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile number must be 10 digits")]
public string Mobile { get; set; }

[Required]
[Display(Name = "Apartment Name")]
public string ApartmentName { get; set; } = null!;
        [Required]
        public string Address { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string HouseType { get; set; }

        [Required]
[RegularExpression(@"^[A-Za-z]{2}[0-9]{2}[A-Za-z]{1,2}[0-9]{4}$", 
    ErrorMessage = "Enter a valid car number (e.g., TN09AC7667)")]
public string CarNumber { get; set; }

        public int Price { get; set; }

        // ?? MAKE DATE OPTIONAL (Fix Model Invalid)
        public DateTime? BookingDate { get; set; }
    }
}