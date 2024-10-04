using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository carRepository;

        public CarsController()
        {
            carRepository = new CarRepository();    
        }

        // GET: api/Cars
        [HttpGet]
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
        [HttpPost]
        public async Task<ActionResult> PostCar([FromBody]Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await carRepository.Add(car);
            return Ok(car);
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
