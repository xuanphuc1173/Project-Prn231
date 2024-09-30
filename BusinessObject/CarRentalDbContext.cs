using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarReview> CarReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập các ràng buộc và quan hệ nếu cần.
            base.OnModelCreating(modelBuilder);
        }
    }
}
