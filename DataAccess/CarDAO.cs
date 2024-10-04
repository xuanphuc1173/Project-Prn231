using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CarDAO: SingletonBase<CarDAO>
    {
        public async Task<IEnumerable<Car>> GetCarAll()
        {
            return await _context.Cars.ToListAsync();
        }
        public async Task<Car> GetCarById(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            return car;
        }
        public async Task Add(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Car car)
        {
            var existingItem = await GetCarById(car.CarId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(car);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var car = await GetCarById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
