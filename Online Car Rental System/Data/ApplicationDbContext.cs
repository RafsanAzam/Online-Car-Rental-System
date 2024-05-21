using Microsoft.EntityFrameworkCore;
using Online_Car_Rental_System.Models;

namespace Online_Car_Rental_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Car> Cars  { get; set; }
    }
}
