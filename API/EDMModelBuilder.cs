using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using BusinessObject;
using static System.Reflection.Metadata.BlobBuilder;
namespace API
{
    public class EDMModelBuilder
    {
        public static IEdmModel GetEDMModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Car>("Cars");
            modelBuilder.EntitySet<CarCategory>("CarCategory");
            modelBuilder.EntitySet<Customer>("Customers");
            modelBuilder.EntitySet<Booking>("Bookings");
            modelBuilder.EntitySet<CarReview>("CarReviews");

            var car = modelBuilder.EntityType<Car>();
            car.HasKey(c => c.CarId);
            car.HasRequired(c => c.Category, (c, cc) => c.CategoryId == cc.CategoryId);

            var carCategory = modelBuilder.EntityType<CarCategory>();
            carCategory.HasKey(cc => cc.CategoryId);

            var customer = modelBuilder.EntityType<Customer>();
            customer.HasKey(cu => cu.CustomerId);

            var booking = modelBuilder.EntityType<Booking>();
            booking.HasKey(b => b.BookingId);
            booking.HasRequired(b => b.Customer, (b, cu) => b.CustomerId == cu.CustomerId);
            booking.HasRequired(b => b.Car, (b, c) => b.CarId == c.CarId);

            var carReview = modelBuilder.EntityType<CarReview>();
            carReview.HasKey(r => r.ReviewId);
            carReview.HasRequired(r => r.Car, (r, c) => r.CarId == c.CarId);
            carReview.HasRequired(r => r.Customer, (r, cu) => r.CustomerId == cu.CustomerId);

            return modelBuilder.GetEdmModel();
        }
    }
}

