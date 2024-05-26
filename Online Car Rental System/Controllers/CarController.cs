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
    }
}
