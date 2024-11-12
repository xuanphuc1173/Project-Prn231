using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace API.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CarCategoriesController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public CarCategoriesController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/CarCategories
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CarCategory>>> GetCarCategories()
        {
            return await _context.CarCategories.ToListAsync();
        }

        // GET: api/CarCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarCategory>> GetCarCategory(int id)
        {
            var carCategory = await _context.CarCategories.FindAsync(id);

            if (carCategory == null)
            {
                return NotFound();
            }

            return carCategory;
        }

        // PUT: api/CarCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarCategory(int id, CarCategory carCategory)
        {
            if (id != carCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(carCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<CarCategory>> PostCarCategory(CarCategory carCategory)
        {
            _context.CarCategories.Add(carCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarCategory", new { id = carCategory.CategoryId }, carCategory);
        }

        // DELETE: api/CarCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarCategory(int id)
        {
            var carCategory = await _context.CarCategories.FindAsync(id);
            if (carCategory == null)
            {
                return NotFound();
            }

            _context.CarCategories.Remove(carCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarCategoryExists(int id)
        {
            return _context.CarCategories.Any(e => e.CategoryId == id);
        }
    }
}
