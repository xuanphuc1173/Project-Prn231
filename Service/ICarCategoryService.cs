using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICarCategoryService
    {
        Task<IEnumerable<CarCategoryDTO>> GetCarCategoryAll();

        Task<CarCategoryDTO> GetCarCategoryById(int id);
        Task Add(CarCategoryDTO car);
        Task Update(CarCategoryDTO car);
        Task Delete(int id);
    }
}
