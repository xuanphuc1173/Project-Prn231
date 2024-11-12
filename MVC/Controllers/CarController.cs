using BusinessObject;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net.Http.Headers;
using static System.Reflection.Metadata.BlobBuilder;

namespace MVC.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        public CarController(ICarService carService)
        { 
            this.carService = carService; 
        }
        // GET: CarController
        public async Task<ActionResult> Index()
        {
            var cars = await carService.GetCarAll();
            return View("~/Views/Admin/Car/Index.cshtml", cars);
        }

        // GET: CarController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var car = await carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Car/Details.cshtml", car);
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View("~/Views/Admin/Car/Create.cshtml");
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarCreate car)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Car/Create.cshtml", car);
            }
            try
            {
                using var content = new MultipartFormDataContent();

                // Thêm các thông tin khác của xe vào nội dung
                content.Add(new StringContent(car.Brand), nameof(car.Brand));
                content.Add(new StringContent(car.Color), nameof(car.Color));
                content.Add(new StringContent(car.LicensePlate), nameof(car.LicensePlate));
                content.Add(new StringContent(car.Description), nameof(car.Description));
                content.Add(new StringContent(car.Status), nameof(car.Status));
                content.Add(new StringContent(car.CreatedDate.ToString()), nameof(car.CreatedDate));
                content.Add(new StringContent(car.PricePerDay.ToString()), nameof(car.PricePerDay));
                content.Add(new StringContent(car.Model.ToString()), nameof(car.Model));
                content.Add(new StringContent(car.Year.ToString()), nameof(car.Year));
                content.Add(new StringContent(car.CategoryId.ToString()), nameof(car.CategoryId));

                // Xử lý hình ảnh
                if (car.ImageUrl != null && car.ImageUrl.Length > 0)
                {
                    try
                    {
                        var memoryStream = new MemoryStream();
                        await car.ImageUrl.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        var fileContent = new StreamContent(memoryStream);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(car.ImageUrl.ContentType);
                        content.Add(fileContent, "ImageUrl", car.ImageUrl.FileName);
                    }
                    catch (IOException ioEx)
                    {
                        ModelState.AddModelError("Image", $"Image processing failed due to IO error: {ioEx.Message}");
                        return View("~/Views/Admin/Car/Create.cshtml", car);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Image", $"Image processing failed: {ex.Message}");
                        return View("~/Views/Admin/Car/Create.cshtml", car);
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please upload a valid image.");
                    return View("~/Views/Admin/Car/Create.cshtml", car);
                }

                try
                {
                    await carService.Add(content);
                    return RedirectToAction(nameof(Index));
                }
                catch (HttpRequestException httpEx)
                {
                    ModelState.AddModelError("", $"HTTP request error: {httpEx.Message}");
                    return View("~/Views/Admin/Car/Create.cshtml", car);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Unexpected error while adding the car: {ex.Message}");
                    return View("~/Views/Admin/Car/Create.cshtml", car);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"General error: {ex.Message}");
                return View("~/Views/Admin/Car/Create.cshtml", car);
            }
        }

        // GET: CarController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var car = await carService.GetCarById(id);
            return View("~/Views/Admin/Car/Edit.cshtml", car);
        }


        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CarDTO car)
        {
            try
            {
                await carService.Update(car);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Admin/Car/Edit.cshtml");

            }
        }
        // GET: CarController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var car = await carService.GetCarById(id);
            return View("~/Views/Admin/Car/Delete.cshtml", car);
        }
        // POST: SHippingController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsk(int id)
        {
            try
            {
                await carService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Admin/Car/Delete.cshtml");
            }
        }
    }
}
