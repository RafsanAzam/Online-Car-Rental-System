namespace Online_Car_Rental_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public bool Status { get; set; }

        // // Navigation property for Car
        public Car Car { get; set; } 

        // Parameterless constructor required by Entity Framework
        public Order()
        {


        }
        public Order(Reservation reservation)
        {
            CarId = reservation.CarId;
            Name = reservation.Name;
            UserEmail = reservation.UserEmail;
            RentStartDate = reservation.RentStartDate;
            RentEndDate = reservation.RentEndDate;
            Quantity = reservation.Quantity;
            TotalPrice = reservation.TotalPrice; 
            Status = false; // Default status, assuming false means not completed
        }
    }
}
