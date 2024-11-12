using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CarService : ICarService
    { 
        private readonly HttpClient _httpClient;
    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<CarDTO>> GetCarAll() => await _httpClient.GetFromJsonAsync<IEnumerable<CarDTO>>("Cars/GetAll");

    public async Task<CarDTO> GetCarById(int id) => await _httpClient.GetFromJsonAsync<CarDTO>($"Cars/{id}");
    public async Task Add(MultipartFormDataContent content) => await _httpClient.PostAsync($"Cars/Add", content);
    public async Task Update(CarDTO car) => await _httpClient.PutAsJsonAsync<CarDTO>($"Cars/{car.CarId}", car);
    public async Task Delete(int id) => await _httpClient.DeleteAsync($"Cars/{id}");
}
}