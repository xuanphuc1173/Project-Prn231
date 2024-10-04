using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CarReview
    {
        public int ReviewId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }  // From 1 to 5
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        // Navigation properties
        public Car Car { get; set; }
        public Customer Customer { get; set; }
    }

}
