using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Service;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            this.carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var car = await carService.GetCarAll();

            return View(car);
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult client()
        {
            return View();
        }

        public IActionResult service()
        {
            return View();
        }

        public IActionResult gallery() 
        {    
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
