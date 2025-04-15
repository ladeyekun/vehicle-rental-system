using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class VehicleRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public VehicleRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Vehicle> GetVehicles() {
            return _context.Vehicles.ToList();
        }

        public List<Vehicle> GetVehicles(int LocationId) {
            return _context.Vehicles
                .Where(v => v.LocationId == LocationId)
                .ToList();
        }

        public Vehicle GetVehicle(int vehicleId) {
            return _context.Vehicles.Find(vehicleId);
        }

        public void AddVehicle(Vehicle vehicle) {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }
        public void UpdateVehicle(Vehicle vehicle) {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
        public void DeleteVehicle(int vehicleId) {
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null) {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

    }
}
