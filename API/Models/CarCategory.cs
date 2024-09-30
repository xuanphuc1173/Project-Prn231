using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class CarCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation properties
        public ICollection<Car> Cars { get; set; }
    }

}
