using System.ComponentModel.DataAnnotations;

namespace Online_Car_Rental_System.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; } // Unique identifier for each car
        public string Type { get; set; } // Type of the car (e.g., SUV, Sedan)
        public string Brand { get; set; } // Brand of the car (e.g., Toyota, Honda)
        public string CarModel { get; set; } // Model of the car (e.g., RAV4, Accord)
        public string Image { get; set; } // URL or path to the car's image
        public int Mileage { get; set; } // Mileage of the car
        public string FuelType { get; set; } // Fuel type of the car (e.g., Petrol, Diesel)
        public int Seats { get; set; } // Number of seats in the car
        public int Quantity { get; set; } // Number of cars available
        public int PricePerDay { get; set; } // Rental price per day
        public string Description { get; set; } // Description of the car
        public string Availability { get; set; } // Availability status of the car
    }
}
