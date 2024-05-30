using Online_Car_Rental_System.Models;

namespace Online_Car_Rental_System.Services.Interfaces
{
    public interface IReservationService
    {
        List<Reservation> GetAllReservations();
        Reservation GetReservationById(int id);
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(int id);
        Reservation GetMostRecentUncompletedReservation(int sessionId);

    }
}
