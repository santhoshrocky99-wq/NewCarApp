using System;
using System.ComponentModel.DataAnnotations;

namespace CarCleanz.Models
{
    public class Booking
    {
[Key]
public int Id { get; set; }
        public string CustomBookingId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string HouseType { get; set; }

        [Required]
        public string CarNumber { get; set; }

        public int Price { get; set; }


        
[DataType(DataType.Date)]
public DateTime? BookingDate { get; set; }

    }
}
