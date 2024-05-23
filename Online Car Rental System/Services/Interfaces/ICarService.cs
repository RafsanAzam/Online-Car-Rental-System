using Online_Car_Rental_System.Models;

namespace Online_Car_Rental_System.Services.Interfaces
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
        List<Car> SearchCars(string keyword);
        List<Car> GetCarsByCategory(string category);
        List<Car> GetCarsByBrand(string brand);

    }
}
