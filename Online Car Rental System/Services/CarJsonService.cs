using Online_Car_Rental_System.Models;
using System.Text.Json;

namespace Online_Car_Rental_System.Services
{
    public class CarJsonService
    {
        private readonly string _filePath = "Online Car Rental System/cars.json";

        public void UpdateJsonFile(List<Car> cars)
        {
            var jsonData = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(_filePath, jsonData);
        }

    }
}
