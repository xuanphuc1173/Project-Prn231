using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
    public CustomerService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CustomerDTO>> GetCustomerAll() => await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDTO>>("Customers/GetAll");

        public async Task<CustomerDTO> GetCustomerById(int id) => await _httpClient.GetFromJsonAsync<CustomerDTO>($"Customers/{id}");
        public async Task Add(CustomerDTO customer) => await _httpClient.PostAsJsonAsync<CustomerDTO>($"Customers/Add", customer);
        public async Task Update(CustomerDTO customer) => await _httpClient.PutAsJsonAsync<CustomerDTO>($"Customers/{customer.CustomerId}", customer);
        public async Task Delete(int id) => await _httpClient.DeleteAsync($"Customers/{id}");
    }
}
