using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CarCategoryService : ICarCategoryService
    {
        private readonly HttpClient _httpClient;
        public CarCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CarCategoryDTO>> GetCarCategoryAll() => await _httpClient.GetFromJsonAsync<IEnumerable<CarCategoryDTO>>("CarCategories/GetAll");

        public async Task<CarCategoryDTO> GetCarCategoryById(int id) => await _httpClient.GetFromJsonAsync<CarCategoryDTO>($"CarCategories/{id}");
        public async Task Add(CarCategoryDTO car) => await _httpClient.PostAsJsonAsync($"CarCategories/Add", car);
        public async Task Update(CarCategoryDTO car) => await _httpClient.PutAsJsonAsync<CarCategoryDTO>($"CarCategories/{car.CategoryId}", car);
        public async Task Delete(int id) => await _httpClient.DeleteAsync($"CarCategories/{id}");
    }
}
