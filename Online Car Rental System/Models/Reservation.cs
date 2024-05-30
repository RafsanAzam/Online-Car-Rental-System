using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Car_Rental_System.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string MobileNumber { get; set; }
        public bool HasValidDriverLicense { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; } // New status field
        public string SessionId { get; set; } // New field for session ID

        [ForeignKey("CarId")]
        public Car Car { get; set; }  // Navigation property
    }
}
