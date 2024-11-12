using BusinessObject;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            var customers = await customerService.GetCustomerAll();
            return View("~/Views/Admin/Customer/Index.cshtml", customers);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var customers = await customerService.GetCustomerById(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Customer/Details.cshtml", customers);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/Customer/Create.cshtml");
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerDTO customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("~/Views/Admin/Customer/Create.cshtml", customer);
                }
                await customerService.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Admin/Customer/Create.cshtml");
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var customers = await customerService.GetCustomerById(id);
            return View("~/Views/Admin/Customer/Edit.cshtml", customers);
        }


        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( CustomerDTO customers)
        {
            try
            {
                await customerService.Update(customers);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Admin/Customer/Edit.cshtml");

            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customers = await customerService.GetCustomerById(id);
            return View("~/Views/Admin/Customer/Delete.cshtml", customers);
        }
        // POST: SHippingController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsk(int id)
        {
            try
            {
                await customerService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Admin/Customer/Delete.cshtml");
            }
        }
    }
}
