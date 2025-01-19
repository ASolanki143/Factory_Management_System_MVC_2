using Factory_Management_System_MVC_2.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Factory_Management_System_MVC_2.Helper;

namespace Factory_Management_System_MVC_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/product/?AdminID=1");
            if (response.IsSuccessStatusCode)
            {
                JsonOperation<List<ProductModel>> jsonOperation = new JsonOperation<List<ProductModel>>();

                ApiResponse<List<ProductModel>> apiResponse = await jsonOperation.jsonDeserialization(response);
                Console.WriteLine(apiResponse.Data);
                if (apiResponse.Success)
                {
                    return View(apiResponse.Data);
                }

            }
            return View(new List<ProductModel>());
        }

        public async Task<IActionResult> Add(int? ProductID)
        {
            Console.WriteLine($"Product ID: {ProductID}");
            if (ProductID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/product/{ProductID}");
                if (response.IsSuccessStatusCode)
                {
                    JsonOperation<ProductModel> jsonOperation = new JsonOperation<ProductModel>();

                    ApiResponse<ProductModel> apiResponse = await jsonOperation.jsonDeserialization(response);
                    if (apiResponse.Success)
                    {
                        return View("AddEditProduct", apiResponse.Data);
                    }
                }
            }
            return View("AddEditProduct", new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductModel product)
        {
            Console.WriteLine("qwertyui");
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(product);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                if (product.ProductID.HasValue)
                {
                    Console.WriteLine("Product ID hase value");
                    var response = await _httpClient.PutAsync($"api/product/update/{product.ProductID}", data);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    Console.WriteLine("Product ID not hase value");

                    var response = await _httpClient.PostAsync("api/product", data);
                    Console.WriteLine(response);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("AddEditProduct", product);
        }
    }
}
