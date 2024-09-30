using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // Navigation properties
        public CarCategory Category { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
