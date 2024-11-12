using Microsoft.AspNetCore.Mvc;
using Service;

namespace MVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly ICarService carService;

        public ClientController(ICarService carService)
        {
            this.carService = carService;

        }
        public async Task<IActionResult> Index()
        {
            var cars = await carService.GetCarAll();

            return View(cars);
        }
        public async Task<ActionResult> Details(int id)
        {
            var car = await carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
    }
}
