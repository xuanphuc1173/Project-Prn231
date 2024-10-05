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
    [Route("api/[controller]")]
    [ApiController]
    public class CarReviewsController : ControllerBase
    {
        private readonly CarRentalDbContext _context;

        public CarReviewsController(CarRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/CarReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarReview>>> GetCarReviews()
        {
            return await _context.CarReviews.ToListAsync();
        }

        // GET: api/CarReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarReview>> GetCarReview(int id)
        {
            var carReview = await _context.CarReviews.FindAsync(id);

            if (carReview == null)
            {
                return NotFound();
            }

            return carReview;
        }

        // PUT: api/CarReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarReview(int id, CarReview carReview)
        {
            if (id != carReview.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(carReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarReviewExists(id))
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

        // POST: api/CarReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarReview>> PostCarReview(CarReview carReview)
        {
            _context.CarReviews.Add(carReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarReview", new { id = carReview.ReviewId }, carReview);
        }

        // DELETE: api/CarReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarReview(int id)
        {
            var carReview = await _context.CarReviews.FindAsync(id);
            if (carReview == null)
            {
                return NotFound();
            }

            _context.CarReviews.Remove(carReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarReviewExists(int id)
        {
            return _context.CarReviews.Any(e => e.ReviewId == id);
        }
    }
}
