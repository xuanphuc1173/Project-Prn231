using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminHome()
        {
            return View();
        }
        public IActionResult Chatbox()
        {
            return View();
        }

    }
}
