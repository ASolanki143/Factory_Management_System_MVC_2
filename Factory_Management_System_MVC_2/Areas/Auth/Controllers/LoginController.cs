using Factory_Management_System_MVC_2.Areas.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Factory_Management_System_MVC_2.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class LoginController : Controller
    {
        //private readonly IConfiguration _configuration;
        //private readonly HttpClient _httpClient;

        //public LoginController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _httpClient = new HttpClient
        //    {
        //        BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
        //    };
        //}
        public IActionResult Index()
        {
            Console.WriteLine("Hello");
            return View();
        }

        [HttpPost]
        public IActionResult Save(LoginModel? model)
        {
            Console.WriteLine("Hello from auth");
            // Logic for authentication
            if (model.username == "admin" && model.password == "password")
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index");
        }
    }
}
