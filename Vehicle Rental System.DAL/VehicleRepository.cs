using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class VehicleRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public VehicleRepository(VehicleRentalSystemDbContext context)
        {
            _context = context;
        }

        // Get All Vehicles
        public async Task<List<Vehicle>> GetVehicles()
        {
            return await _context.Vehicles
                .Include(v => v.Reservations)
                .Include(v => v.Histories)
                .Include(v => v.Location)
                .ToListAsync();
        }

        // Get Vehicle by ID
        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Reservations)
                .Include(v => v.Histories)
                .Include(v => v.Location)
                .FirstOrDefaultAsync(v => v.VehicleId == id);
        }

        // Add New Vehicle
        public async Task AddAsync(Vehicle vehicle, List<int> reservationIds, List<int> historyIds)
        {
            vehicle.Reservations = new List<Reservation>();
            vehicle.Histories = new List<History>();

            // Add the associated reservations
            foreach (int resId in reservationIds)
            {
                Reservation reservation = await _context.Reservations.FindAsync(resId);
                if (reservation != null)
                {
                    vehicle.Reservations.Add(reservation);
                }
            }

            // Add the associated histories
            foreach (int histId in historyIds)
            {
                History history = await _context.Histories.FindAsync(histId);
                if (history != null)
                {
                    vehicle.Histories.Add(history);
                }
            }

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        // Update Vehicle
        public async Task UpdateAsync(Vehicle vehicle, List<int> reservationIds, List<int> historyIds)
        {
            Vehicle existingVehicle = await _context.Vehicles
                .Include(v => v.Reservations)
                .Include(v => v.Histories)
                .FirstOrDefaultAsync(v => v.VehicleId == vehicle.VehicleId);

            if (existingVehicle == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            // Update the vehicle details
            existingVehicle.Brand = vehicle.Brand;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.Type = vehicle.Type;
            existingVehicle.RentalPrice = vehicle.RentalPrice;
            existingVehicle.VehicleAvailabilityStatus = vehicle.VehicleAvailabilityStatus;
            existingVehicle.Mileage = vehicle.Mileage;
            existingVehicle.LocationId = vehicle.LocationId;

            // Clear and reassign the associated reservations
            existingVehicle.Reservations.Clear();
            foreach (int resId in reservationIds)
            {
                Reservation reservation = await _context.Reservations.FindAsync(resId);
                if (reservation != null)
                {
                    existingVehicle.Reservations.Add(reservation);
                }
            }

            // Clear and reassign the associated histories
            existingVehicle.Histories.Clear();
            foreach (int histId in historyIds)
            {
                History history = await _context.Histories.FindAsync(histId);
                if (history != null)
                {
                    existingVehicle.Histories.Add(history);
                }
            }

            await _context.SaveChangesAsync();
        }

        // Delete Vehicle
        public async Task DeleteAsync(int id)
        {
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        // Get Reservations for a Vehicle
        public async Task<List<Reservation>> GetReservationsAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Reservations)
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            return vehicle?.Reservations.ToList() ?? new List<Reservation>();
        }

        // Get Histories for a Vehicle
        public async Task<List<History>> GetHistoriesAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Histories)
                .FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            return vehicle?.Histories.ToList() ?? new List<History>();
        }

        // Get Reservation by ID
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        // Get History by ID
        public async Task<History> GetHistoryByIdAsync(int id)
        {
            return await _context.Histories.FindAsync(id);
        }

    }
}
