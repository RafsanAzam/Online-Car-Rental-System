using Microsoft.EntityFrameworkCore;
using Online_Car_Rental_System.Data;
using Online_Car_Rental_System.Services;
using Online_Car_Rental_System.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options=> 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register Application Services
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<CarJsonService>();


var app = builder.Build();


/*Update Car table from JSON file during startup
using (var scope = app.Services.CreateScope())
{
    var carService = scope.ServiceProvider.GetRequiredService<ICarService>();
    carService.updateCarTableFromJson();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();
