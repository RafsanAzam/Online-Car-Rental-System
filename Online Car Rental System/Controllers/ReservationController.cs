using Microsoft.AspNetCore.Mvc;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;

        public ReservationController(IReservationService reservationService, ICarService carService)
        {
            _reservationService = reservationService;
            _carService = carService;
        }
        
        public IActionResult Create()
        {
            var cars = _carService.GetAllCars();
            ViewBag.Cars = cars;
            return View(new ReservationViewModel());
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
                    Status = "Unconfirmed" // Default status
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
            var cars = _carService.GetAllCars();
            ViewBag.Cars = cars;
            return View(reservation);
        }

        [HttpPost]
        public IActionResult Edit(Reservation reservation)
        {
            if(ModelState.IsValid)
            {
                _reservationService.UpdateReservation(reservation);
                return RedirectToAction("Details", new {id = reservation.ReservationId});
            }
            return View(reservation);
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
    }
}
