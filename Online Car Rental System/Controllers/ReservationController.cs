using Microsoft.AspNetCore.Mvc;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services;
using Online_Car_Rental_System.Services.Interfaces;
using Newtonsoft.Json;



namespace Online_Car_Rental_System.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly CarJsonService _carJsonService;

        public ReservationController(IReservationService reservationService, ICarService carService, CarJsonService carJsonService)
        {
            _reservationService = reservationService;
            _carService = carService;
            _carJsonService = carJsonService;
        }

        [HttpGet]
        [Route("Reservation/Create/{carId}")]
        public IActionResult Create(int carId)
        {
            var car = _carService.GetCarById(carId);

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = new ReservationViewModel
            {
                CarId = car.CarId,
                TotalPrice = car.PricePerDay // Assuming you want to initialize TotalPrice with the car's price per day
            };

            ViewBag.Car = car; // Pass the car details to the view using ViewBag
            return View(viewModel);
        }




        [HttpPost]
        public IActionResult Create(ReservationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    CarId = viewModel.CarId,
                    Name = viewModel.Name,
                    UserEmail = viewModel.UserEmail,
                    MobileNumber = viewModel.MobileNumber,
                    HasValidDriverLicense = viewModel.HasValidDriverLicense,
                    RentStartDate = viewModel.RentStartDate,
                    RentEndDate = viewModel.RentEndDate,
                    Quantity = viewModel.Quantity,
                    TotalPrice = viewModel.TotalPrice,
                    Status = "Unconfirmed", // Default status
                    SessionId = HttpContext.Session.Id // Set the SessionId

                };

                _reservationService.AddReservation(reservation);
                return RedirectToAction("Details", new { id = reservation.ReservationId });
            }
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if(reservation == null)
            {
                return NotFound();
            }
            var viewModel = new ReservationViewModel
            {
                ReservationId = reservation.ReservationId,
                CarId = reservation.CarId,
                Name = reservation.Name,
                UserEmail = reservation.UserEmail,
                MobileNumber = reservation.MobileNumber,
                HasValidDriverLicense = reservation.HasValidDriverLicense,
                RentStartDate = reservation.RentStartDate,
                RentEndDate = reservation.RentEndDate,
                Quantity = reservation.Quantity,
                TotalPrice = reservation.TotalPrice
            };
            ViewBag.Car = _carService.GetCarById(reservation.CarId); // Pass the car details to the view using ViewBag
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ReservationViewModel viewModel)
        {
           if (ModelState.IsValid)
            {
                var reservation = _reservationService.GetReservationById(viewModel.ReservationId);
                if(reservation == null)
                {
                    return NotFound();
                }

                reservation.Name = viewModel.Name;
                reservation.UserEmail = viewModel.UserEmail;
                reservation.MobileNumber = viewModel.MobileNumber;
                reservation.HasValidDriverLicense = viewModel.HasValidDriverLicense;
                reservation.RentStartDate = viewModel.RentStartDate;
                reservation.RentEndDate = viewModel.RentEndDate;
                reservation.Quantity = viewModel.Quantity;
                reservation.TotalPrice = viewModel.TotalPrice;
                _reservationService.UpdateReservation(reservation);
                return RedirectToAction("Details", new {id= reservation.ReservationId});
            }
            ViewBag.Car = _carService.GetCarById(viewModel.CarId); // Pass the car details to the view using ViewBag
            return View(viewModel);

        }

        public IActionResult Details(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if(reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

       

        public IActionResult Delete(int id)
        {
            _reservationService.DeleteReservation(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Recent()
        {
            return RedirectToAction("Edit");
        }

        public IActionResult Confirm(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if(reservation == null)
            {
                return NotFound();
            }
            reservation.Status = "Confirmed";
            _reservationService.UpdateReservation(reservation);

            //Update car Availability
            var car = _carService.GetCarById(reservation.CarId);
            car.Quantity -= reservation.Quantity;
            _carService.UpdateCar(car);

            //Update the car data in JSON
            _carJsonService.UpdateJsonFile(_carService.GetAllCars());
            
            return RedirectToAction("Details", new {id = reservation.ReservationId});   
        }
    }
}
