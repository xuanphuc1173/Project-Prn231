using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string DriverLicenseNumber { get; set; }
        public int Type { get; set; }


        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
    }

}
