using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
	public class Account : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
