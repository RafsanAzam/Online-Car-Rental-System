using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Car_Rental_System.Models
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public int CarId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "You must have a valid driver's license to place a rental order")]
        public bool HasValidDriverLicense { get; set; }

        [Required(ErrorMessage = "Rent start date is required")]
        [DataType(DataType.Date)]
        public DateTime RentStartDate { get; set; }

        [Required(ErrorMessage = "Rent end date is required")]
        [DataType(DataType.Date)]
        public DateTime RentEndDate { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public int TotalPrice { get; set; } // Changed to decimal for price accuracy
    }
}
