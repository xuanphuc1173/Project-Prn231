using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Models
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
            // Định nghĩa khóa chính cho mỗi bảng
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Car>()
                .HasKey(c => c.CarId);

            modelBuilder.Entity<CarCategory>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            modelBuilder.Entity<CarReview>()
                .HasKey(r => r.ReviewId);

            base.OnModelCreating(modelBuilder);
        }

    }

}
