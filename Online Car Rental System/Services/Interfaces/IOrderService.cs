namespace Online_Car_Rental_System.Services.Interfaces
{
    public interface IOrderService
    {
        //Method to create an order from a reservation ID
        void CreateOrderFromReservation(int  reservationId);

        //Method to confirm an order by its ID
        void ConfirmOrder(int id);
    }
}
