using Online_Car_Rental_System.Models;
using System.Text.Json;

namespace Online_Car_Rental_System.Services
{
    public class CarJsonService
    {
        private readonly string _filePath;

        public CarJsonService()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "cars.json");
        }

        public List<Car> ReadJsonFile()
        {
            if(!File.Exists(_filePath))
            {
                throw new FileNotFoundException("The cars.json file was not found");
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Car>>(jsonData);
        }

        public void UpdateJsonFile(List<Car> cars)
        {
            var jsonData = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(_filePath, jsonData);
        }

    }
}
