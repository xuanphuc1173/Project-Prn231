using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using DTO;

namespace API.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository carRepository;
        private readonly IWebHostEnvironment _env;

        public CarsController(IWebHostEnvironment env)
        {
            carRepository = new CarRepository();    
            _env = env;
        }

        // GET: api/Cars
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Car>> GetCars()
        {
            var car = await carRepository.GetCarAll();
            return car;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await carRepository.GetCarById(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id,[FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = await carRepository.GetCarById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            car.CarId = id;
            await carRepository.Update(car);

            return Ok("Update success!");
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult> PostCar([FromForm] CarCreate car)
        { 
            if (car.ImageUrl == null || car.ImageUrl.Length == 0)
            {
                return BadRequest("No image uploaded.");
    }

            // Chuyển đổi hình ảnh thành chuỗi Base64
            using (var memoryStream = new MemoryStream())
            {
                await car.ImageUrl.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            var base64Image = Convert.ToBase64String(imageBytes);

            // Tạo đối tượng sản phẩm mới từ thông tin trong model
            var newBook = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                LicensePlate = car.LicensePlate,
                Status = car.Status,
                PricePerDay = car.PricePerDay,
                Description = car.Description,
                CreatedDate = car.CreatedDate,
                CategoryId=car.CategoryId,
                ImageUrl = base64Image, 
            };

            // Lưu sản phẩm vào cơ sở dữ liệu
            await carRepository.Add(newBook);
                return Ok(newBook); // Trả về sản phẩm đã thêm
}
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var exist = await carRepository.GetCarById(id);
            if (exist == null)
            {
                return NotFound();
            }
            await carRepository.Delete(id);
            return Ok("Delete success");
        }


    }
}
