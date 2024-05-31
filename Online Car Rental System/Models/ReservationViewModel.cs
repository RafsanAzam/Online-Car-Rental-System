using System.ComponentModel.DataAnnotations;

namespace Online_Car_Rental_System.Models
{
    public class ReservationViewModel
    {
        public int? ReservationId { get; }
        public int CarId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public bool HasValidDriverLicense { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RentStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RentEndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public int TotalPrice { get; set; }
    }
}
