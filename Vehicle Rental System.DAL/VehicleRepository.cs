using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class VehicleRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public VehicleRepository(VehicleRentalSystemDbContext context)
        {
            _context = context;
        }

        // Get All Vehicle

        public async Task<List<Vehicle>> GetAllAsync()
        {
            List<Vehicle> vehicles = await _context.Vehicles
                .Include(v => v.Reservations)
                .Include(v => v.Histories)
                .Include(v => v.Location)
                .ToListAsync();

            return vehicles;
        }

        // Get Vehicle By Assigned Id

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            Vehicle vehicle = await _context.Vehicles
                .Include(v => v.Reservations)
                .Include(v => v.Histories)
                .Include(v => v.Location)
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            return vehicle;
        }

        // Add New Vehicle

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        // Update Vehicle Info

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        // Delete Vehicle

        public async Task DeleteAsync(int id)
        {
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }

    }
}
