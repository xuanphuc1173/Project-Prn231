using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
namespace Repositories
{
    public class CarRepository: ICarRepository
    {
        public async Task<IEnumerable<Car>> GetCarAll()
        {
            return await CarDAO.Instance.GetCarAll();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await CarDAO.Instance.GetCarById(id);
        }

        public async Task Add(Car car)
        {
            await CarDAO.Instance.Add(car);
        }

        public async Task Update(Car car)
        {
            await CarDAO.Instance.Update(car);
        }

        public async Task Delete(int id)
        {
            await CarDAO.Instance.Delete(id);
        }
    }
}
