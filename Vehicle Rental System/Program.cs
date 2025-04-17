using Vehicle_Rental_System.DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.BLL;
using Microsoft.AspNetCore.Identity;

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

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() // Add support for roles
                .AddEntityFrameworkStores<VehicleRentalSystemDbContext>();

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

            // Seed roles and admin user here
            SeedRolesAndAdminUserAsync(app.Services).GetAwaiter().GetResult();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Reservation}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }

        static async Task SeedRolesAndAdminUserAsync(IServiceProvider serviceProvider) {
            using (IServiceScope scope = serviceProvider.CreateScope()) {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Define roles
                string[] roles = { "Admin", "Support", "Customer" };
                foreach (string role in roles) {
                    if (!await roleManager.RoleExistsAsync(role)) {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Create an admin user
                IdentityUser adminUser = new IdentityUser {
                    UserName = "admin@abc.com",
                    Email = "admin@abc.com",
                    EmailConfirmed = true
                };
                if (await userManager.FindByEmailAsync(adminUser.Email) == null) {
                    await userManager.CreateAsync(adminUser, "AdminPassword123!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

    }
}
