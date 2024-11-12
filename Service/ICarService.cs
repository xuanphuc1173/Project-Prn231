using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICarService 
    {
        Task<IEnumerable<CarDTO>> GetCarAll();

        Task<CarDTO> GetCarById(int id);
        Task Add(MultipartFormDataContent content);
        Task Update(CarDTO car);
        Task Delete(int id);
    }
}
