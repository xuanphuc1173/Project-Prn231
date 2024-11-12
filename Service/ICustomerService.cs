using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomerAll();

        Task<CustomerDTO> GetCustomerById(int id);
        Task Add(CustomerDTO customer);
        Task Update(CustomerDTO customer);
        Task Delete(int id);
    }
}
