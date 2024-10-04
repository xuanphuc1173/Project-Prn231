using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CarReviewDAO: SingletonBase<CarReviewDAO>
    {
        public async Task<IEnumerable<CarReview>> GetReviewAll()
        {
            return await _context.CarReviews.ToListAsync();
        }
        public async Task<CarReview> GetReviewById(int id)
        {
            var car = await _context.CarReviews.FirstOrDefaultAsync(c => c.ReviewId == id);
            return car;
        }
        public async Task Add(CarReview car)
        {
            _context.CarReviews.Add(car);
            await _context.SaveChangesAsync();
        }
        public async Task Update(CarReview car)
        {
            var existingItem = await GetReviewById(car.ReviewId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(car);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var car = await GetReviewById(id);
            if (car != null)
            {
                _context.CarReviews.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
