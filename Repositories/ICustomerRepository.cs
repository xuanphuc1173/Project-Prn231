using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomerAll();
        Task<IEnumerable<Customer>> GetCustomerByType(int type);
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByEmail(string Email);
        Task<Customer> ValidateUser(string Email, string Password);
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(int id);
        Task<Customer> GetUserByEmail(string email);
        Task<Customer> GetCustomerByEmailAndPassword(string email, string password);
    }
}
