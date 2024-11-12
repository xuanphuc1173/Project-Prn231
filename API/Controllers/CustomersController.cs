using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using BusinessObject;
using DTO;
using Repositories;
namespace API.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly  ICustomerRepository customerRepository;

        public CustomersController()
        {
            customerRepository = new CustomerRepository();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            var customer = await customerRepository.GetCustomerByEmailAndPassword(dto.Email, dto.Password);

            if (customer == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Trả về DTO response
            var responseDto = new LoginDTO
            {
                FullName = customer.FullName,
                Email = customer.Email,
                Type = customer.Type,
                CustomerId = customer.CustomerId,
                Password = customer.Password,
            };

            return Ok(responseDto);
        }
        // GET: api/Customers
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customer = await customerRepository.GetCustomerAll();

            return customer;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await customerRepository.GetCustomerById(id);

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exist = await customerRepository.GetCustomerById(id);
            if (exist == null)
            {
                return NotFound();
            }

            customer.CustomerId = id;
            await customerRepository.Update(customer);

            return Ok("Update success!");
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingMember = await customerRepository.GetCustomerByEmail(customer.Email);
            if (existingMember != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return BadRequest(ModelState);
            }
            await customerRepository.Add(customer);

            return Ok(customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var exist = await customerRepository.GetCustomerById(id);
            if (exist == null)
            {
                return NotFound();
            }
            await customerRepository.Delete(id);
            return Ok("Delete success");
        }

       
    }
}
