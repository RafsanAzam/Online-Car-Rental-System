using Online_Car_Rental_System.Data;
using Online_Car_Rental_System.Models;
using Online_Car_Rental_System.Services.Interfaces;

namespace Online_Car_Rental_System.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly CarJsonService _carJsonService;

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
            return _context.Cars
                .Where(c => c.CarModel.Contains(keyword) || c.Brand.Contains(keyword) || c.Type.Contains(keyword)).ToList();
        }

        public List<Car> GetCarsByCategory(string Category)
        {
            return _context.Cars
                .Where(c => c.Type.Equals(Category, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Car> GetCarsByBrand(string Brand)
        {
            return _context.Cars
                .Where(c=> c.Brand.Equals(Brand, System.StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
    }
}
