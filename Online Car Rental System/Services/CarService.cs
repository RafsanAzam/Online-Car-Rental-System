using Online_Car_Rental_System.Data;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly CarJsonService _carJsonService;
        private static List<string> recentSearches = new List<string>(); // Store recent searches

        public CarService(ApplicationDbContext context, CarJsonService carJsonService)
        {
            _context = context;
            _carJsonService = carJsonService;
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.CarId == id);

        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            _carJsonService.UpdateJsonFile(_context.Cars.ToList());
        }

        public void UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            _carJsonService.UpdateJsonFile(_context.Cars.ToList());
        }

        public void DeleteCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                _carJsonService.UpdateJsonFile(_context.Cars.ToList());
            }
        }

        public List<Car> SearchCars(string keyword)
        {
            // Add the keyword to recent searches
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                recentSearches.Add(keyword);
                if (recentSearches.Count > 10)
                {
                    recentSearches.RemoveAt(0);
                }
            }
            return _context.Cars
                .Where(c => c.CarModel.Contains(keyword) || c.Brand.Contains(keyword) || c.Type.Contains(keyword)).ToList();
        }

        public List<Car> GetCarsByCategory(string category)
        {
            return _context.Cars
                .Where(c => c.Type.ToLower() == category.ToLower())
                .ToList();
        }

        public List<Car> GetCarsByBrand(string Brand)
        {
            return _context.Cars
              .Where(c => c.Brand.ToLower() == Brand.ToLower())
              .ToList();
        }

        public void updateCarTableFromJson()
        {
            try
            {
                var carsFromJson = _carJsonService.ReadJsonFile();
                foreach(var car in carsFromJson)
                {
                    var existingCar = _context.Cars.FirstOrDefault(c =>c.CarId == car.CarId);
                    if(existingCar != null)
                    {
                        //Update existing car
                        existingCar.Type = car.Type;
                        existingCar.Brand = car.Brand;
                        existingCar.CarModel = car.CarModel;
                        existingCar.Image = car.Image;
                        existingCar.Mileage = car.Mileage;
                        existingCar.FuelType = car.FuelType;
                        existingCar.Seats = car.Seats;
                        existingCar.Quantity = car.Quantity;
                        existingCar.PricePerDay = car.PricePerDay;
                        existingCar.Description = car.Description;
                        existingCar.Availability = car.Availability;
                    }

                    else
                    {
                        // Add new car
                        _context.Cars.Add(car);
                    }
                }
                _context.SaveChanges();
            }

            catch (Exception ex) 
            {
                throw new Exception("Error updating car table from json file.", ex);
            }
        }

        public List<string> GetSuggestions(string query)
        {
            return _context.Cars
                .Where(c => c.CarModel.Contains(query) || c.Brand.Contains(query) || c.Type.Contains(query))
                .Select(c => c.CarModel)
                .Distinct()
                .Take(5) // Limit to top 5 suggestions
                .ToList();
        }

        public List<string> GetRecentSearches()
        {
            return recentSearches
                .Distinct()
                .ToList();
        }

    }
}
