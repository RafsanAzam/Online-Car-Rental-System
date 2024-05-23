using Microsoft.AspNetCore.Mvc;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;
using System.Diagnostics;

namespace Online_Car_Rental_System.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;   

      /*  public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } */

        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Index()
        {
            var cars = _carService.GetAllCars();
            return View(cars);
        }

        public IActionResult Search(string keyword)
        {
            var cars = _carService.SearchCars(keyword);
            return View(cars);
        }

        public IActionResult Category(string category)
        {
            var cars = _carService.GetCarsByCategory(category);
            return View(cars);
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