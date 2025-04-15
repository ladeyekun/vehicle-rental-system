using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class VehicleService {
        private readonly VehicleRepository _vehicleRepository;

        public VehicleService(VehicleRepository vehicleRepository) {
            _vehicleRepository = vehicleRepository;
        }
        public List<Vehicle> GetVehicles() {
            return _vehicleRepository.GetVehicles();
        }

        public List<Vehicle> GetVehicles(int LocationId) {
            return _vehicleRepository.GetVehicles(LocationId);
        }

        public Vehicle GetVehicle(int vehicleId) {
            return _vehicleRepository.GetVehicle(vehicleId);
        }

        public void AddVehicle(Vehicle vehicle) {
            _vehicleRepository.AddVehicle(vehicle);
        }
        public void UpdateVehicle(Vehicle vehicle) {
            _vehicleRepository.UpdateVehicle(vehicle);
        }
        public void DeleteVehicle(int vehicleId) {
            _vehicleRepository.DeleteVehicle(vehicleId);
        }
    }
}
