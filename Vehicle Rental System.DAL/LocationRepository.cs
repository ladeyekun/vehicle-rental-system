using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class LocationRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public LocationRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Location> GetLocations() {
            return _context.Locations.ToList();
        }

        public Location GetLocation(int id) {
            return _context.Locations.Find(id);
        }

        public void AddLocation(Location location) {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }
        public void UpdateLocation(Location location) {
            _context.Locations.Update(location);
            _context.SaveChanges();
        }
        public void DeleteLocation(int id) {
            Location location = _context.Locations.Find(id);
            if (location != null) {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
        }
    }
}
