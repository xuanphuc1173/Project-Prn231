using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;
namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetCustomerAll()
        {
            return await CustomerDAO.Instance.GetCustomerAll();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await CustomerDAO.Instance.GetCustomerById(id);
        }
        public async Task<Customer> GetCustomerByEmail(string Email)
        {
            return await CustomerDAO.Instance.GetCustomerByEmail(Email);
        }
        public async Task<IEnumerable<Customer>> GetCustomerByType(int type)
        {
            return await CustomerDAO.Instance.GetCustomerByType(type);
        }
        public async Task<Customer> ValidateUser(string Email, string Password)
        {
            return await CustomerDAO.Instance.ValidateUser(Email, Password);
        }

        public async Task Add(Customer customer)
        {
            await CustomerDAO.Instance.Add(customer);
        }

        public async Task Update(Customer customer)
        {
            await CustomerDAO.Instance.Update(customer);
        }

        public async Task Delete(int id)
        {
            await CustomerDAO.Instance.Delete(id);
        }

        public async Task<Customer> GetUserByEmail(string email)
        {
            return await CustomerDAO.Instance.GetCustomerByEmail(email);
        }
        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            return await CustomerDAO.Instance.GetCustomerByEmailAndPassword(email, password);
        }
    }
}
