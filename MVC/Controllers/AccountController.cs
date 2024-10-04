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
        private readonly string CustomerApiUrl = "https://localhost:7296/api/Customers";

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
                return View(dto);
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// Gửi yêu cầu đăng nhập
			var response = await client.PostAsJsonAsync($"{CustomerApiUrl}/Login", dto);  // Sử dụng URI tuyệt đối

			if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginDTO>();

                // Lưu thông tin vào session
                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetInt32("Type", result.Type);
                HttpContext.Session.SetInt32("MemberId", result.CustomerId);
                HttpContext.Session.SetString("Password", result.Password);
                Console.WriteLine(result);
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Lấy thông báo lỗi từ server
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Invalid Email or Password! {errorMessage}";
                return View("~/Views/Account/Index.cshtml",dto);
            }
        }


    }
}
