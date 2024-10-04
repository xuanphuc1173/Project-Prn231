using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CarCategoryDAO: SingletonBase<CarCategoryDAO>
    {
        public async Task<IEnumerable<CarCategory>> GetCategoryAll()
        {
            return await _context.CarCategories.ToListAsync();
        }
        public async Task<CarCategory> GetCategoryById(int id)
        {
            var car = await _context.CarCategories.FirstOrDefaultAsync(c => c.CategoryId == id);
            return car;
        }
        public async Task Add(CarCategory car)
        {
            _context.CarCategories.Add(car);
            await _context.SaveChangesAsync();
        }
        public async Task Update(CarCategory car)
        {
            var existingItem = await GetCategoryById(car.CategoryId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(car);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var car = await GetCategoryById(id);
            if (car != null)
            {
                _context.CarCategories.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
