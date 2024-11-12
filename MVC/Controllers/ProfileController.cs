using BusinessObject;
using DTO;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICustomerService customerService;

        public ProfileController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        public async Task<ActionResult> Index(int id)
        {
            var user = await customerService.GetCustomerById(id); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await customerService.GetCustomerById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerDTO cus)
        {
            try
            {
                await customerService.Update(cus);
                return RedirectToAction(nameof(Index), new { id = cus.CustomerId });
            }
            catch
            {
                return View(cus);
            }
        }
    }
}
