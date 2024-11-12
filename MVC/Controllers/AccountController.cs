using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using DTO;

namespace MVC.Controllers
{
	public class AccountController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string CustomerApiUrl = "https://localhost:7296/odata/Customers";

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public IActionResult Index()
		{
			return View();
		}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginDTO dto)
        {
            // Kiểm tra tính hợp lệ của ModelState
            if (!ModelState.IsValid)
            {
                // Nếu không hợp lệ, quay lại view với lỗi
                TempData["ErrorMessage"] = "Please provide both email and password.";
                return View("~/Views/Account/Index.cshtml", dto);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// Gửi yêu cầu đăng nhập
			var response = await client.PostAsJsonAsync($"{CustomerApiUrl}/Login", dto);  // Sử dụng URI tuyệt đối

			if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginDTO>();

                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetString("FullName", result.FullName);
                HttpContext.Session.SetInt32("Type", result.Type);
                HttpContext.Session.SetInt32("CustomerId", result.CustomerId);
                HttpContext.Session.SetString("Password", result.Password);
                if (result.Type == 1)
                {
                    TempData["SuccessMessage"] = "Login successful!";
                    TempData["FullName"] = "Welcome " + result.FullName;
                    return RedirectToAction("Index", "Client");
                }
                else if (result.Type == 0)
                {
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("AdminHome", "Admin");
                }
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Invalid Email or Password! {errorMessage}";
                return View("~/Views/Account/Index.cshtml",dto);
            }
            TempData["ErrorMessage"] = "Unexpected error. Please try again.";
            return View("~/Views/Account/Index.cshtml", dto);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Type");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
