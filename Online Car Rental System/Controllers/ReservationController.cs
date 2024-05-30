﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Recent()
        {
            string sessionId = HttpContext.Session.Id; // Get the current session ID
            var reservation = _reservationService.GetMostRecentUncompletedReservation(sessionId);
            if (reservation == null)
            {
                return RedirectToAction("Create");
            }

            var car = _carService.GetCarById(reservation.CarId);
            ViewBag.CarAvailability = car.Availability;
            return View("Create", reservation);
        }
    }
}
