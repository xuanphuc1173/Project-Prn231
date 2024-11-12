using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
    public CustomerService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CustomerDTO>> GetCustomerAll() => await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDTO>>("Customer");

        public async Task<CustomerDTO> GetCustomerById(int id) => await _httpClient.GetFromJsonAsync<CustomerDTO>($"Customer/{id}");
        public async Task Add(CustomerDTO customer) => await _httpClient.PostAsJsonAsync<CustomerDTO>($"Customer", customer);
        public async Task Update(CustomerDTO customer) => await _httpClient.PutAsJsonAsync<CustomerDTO>($"Customer/{customer.CustomerId}", customer);
        public async Task Delete(int id) => await _httpClient.DeleteAsync($"Customer/{id}");
    }
}
