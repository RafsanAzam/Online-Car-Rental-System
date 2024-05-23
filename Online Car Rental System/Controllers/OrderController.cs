using Microsoft.AspNetCore.Mvc;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Action to create an order from a reservation

        [HttpPost]
        public IActionResult CreateOrderFromReservation(int reservationId)
        {
            _orderService.CreateOrderFromReservation(reservationId);
            return RedirectToAction("Index", "Home");
        }

        //Action to confirm an order
        public IActionResult ConfirmOrder(int id)
        {
            _orderService.ConfirmOrder(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
