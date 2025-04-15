using Vehicle_Rental_System.DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.BLL;

namespace Vehicle_Rental_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<VehicleRentalSystemDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<CustomerRepository>();
            builder.Services.AddScoped<VehicleRepository>();
            builder.Services.AddScoped<HistoryRepository>();
            builder.Services.AddScoped<LocationRepository>();
            builder.Services.AddScoped<PaymentRepository>();
            builder.Services.AddScoped<ReservationRepository>();
            builder.Services.AddScoped<ReviewRepository>();

            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<VehicleService>();
            builder.Services.AddScoped<HistoryService>();
            builder.Services.AddScoped<LocationService>();
            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddScoped<ReservationService>();
            builder.Services.AddScoped<ReviewService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
