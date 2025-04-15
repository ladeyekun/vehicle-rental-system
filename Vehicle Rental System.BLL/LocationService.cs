using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<Location> GetAllLocations()
        {
            return _locationRepository.GetLocations();
        }

        public Location GetLocationById(int id)
        {
            return _locationRepository.GetLocation(id);
        }

        public void CreateLocation(Location location)
        {
            _locationRepository.AddLocation(location);
        }

        public void UpdateLocation(Location location)
        {
            _locationRepository.UpdateLocation(location);
        }

        public void DeleteLocation(int id)
        {
            _locationRepository.DeleteLocation(id);
        }
    }
}
