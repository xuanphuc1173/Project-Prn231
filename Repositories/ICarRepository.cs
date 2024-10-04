using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCarAll();
        Task<Car> GetCarById(int id);
        Task Add(Car car);
        Task Update(Car car);
        Task Delete(int id);
    }
}
