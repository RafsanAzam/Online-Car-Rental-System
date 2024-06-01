using Microsoft.AspNetCore.Mvc;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Index()
        {
            var cars = _carService.GetAllCars();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }
       

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if(ModelState.IsValid)
            {
                _carService.AddCar(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.UpdateCar(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            if(car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet]
        public JsonResult GetSuggestions(string query)
        {
            var suggestions = _carService.GetSuggestions(query);
            return Json(suggestions);
        }

        [HttpGet]
        public JsonResult GetRecentSearches()
        {
            var recentSearches = _carService.GetRecentSearches();
            return Json(recentSearches);
        }

        public IActionResult Search(string searchTerm)
        {
            var results = _carService.SearchCars(searchTerm);
            if (results == null || !results.Any())
            {
                results = _carService.GetAllCars(); // Assuming you have a method to get all cars
            }
            return View("SearchResults", results);
        }

        public IActionResult Type(string type)
        {
            var cars = _carService.GetCarsByCategory(type);
            return View("SearchResults", cars); 
        }

        public IActionResult Brand(string brand)
        {
            var cars = _carService.GetCarsByBrand(brand);
            return View("SearchResults", cars); 
        }

    }
}
