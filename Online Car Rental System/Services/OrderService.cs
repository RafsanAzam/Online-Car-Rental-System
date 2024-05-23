using Microsoft.AspNetCore.Identity;
using Online_Car_Rental_System.Data;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly CarJsonService _carJsonService;

        public OrderService(ApplicationDbContext context, CarJsonService carJsonService)
        {
            _context = context;
            _carJsonService = carJsonService;
        }

        public void CreateOrderFromReservation(int reservationId)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            var order = new Order(reservation); // Create a new order using the reservation details

            _context.Orders.Add(order); // Add the new order to the Orders DbSet
            _context.SaveChanges();   // Save changes to the database
        }

        public void ConfirmOrder(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(r => r.OrderId == orderId);
            if(order != null)
            {
                order.Status = true;
                _context.SaveChanges();
            }
        }

        private void UpdateCarAvailability(int carId, int quantity)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == carId);
            if (car != null && car.Quantity>=quantity)
            {
                car.Quantity -=quantity;
                _context.SaveChanges();
                _carJsonService.UpdateJsonFile(_context.Cars.ToList());
            }
            else
            {
                throw new Exception("Not Enough Cars Available.");
            }
        }
    }
}
