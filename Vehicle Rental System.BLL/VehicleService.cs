using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class VehicleService {
        private readonly VehicleRepository _vehicleRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly HistoryRepository _historyRepository;

        public VehicleService(
            VehicleRepository vehicleRepository,
            LocationRepository locationRepository,
            ReservationRepository reservationRepository,
            HistoryRepository historyRepository)
        {
            _vehicleRepository = vehicleRepository;
            _locationRepository = locationRepository;
            _reservationRepository = reservationRepository;
            _historyRepository = historyRepository;
        }

        // Get All Vehicles
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _vehicleRepository.GetVehicles();
        }

        // Get Vehicle By Id
        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle must have a valid ID.");

            Vehicle vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
                throw new ArgumentException("Vehicle not found.");

            return vehicle;
        }

        // Add a New Vehicle
        public async Task AddVehicleAsync(Vehicle vehicle, List<int> reservationIds, List<int> historyIds)
        {
            ValidateVehicle(vehicle);
            await _vehicleRepository.AddAsync(vehicle, reservationIds, historyIds);
        }

        // Update an Existing Vehicle
        public async Task UpdateVehicleAsync(Vehicle vehicle, List<int> reservationIds, List<int> historyIds)
        {
            if (vehicle.VehicleId <= 0)
                throw new ArgumentException("Vehicle ID is required for update.");

            ValidateVehicle(vehicle);
            await _vehicleRepository.UpdateAsync(vehicle, reservationIds, historyIds);
        }

        // Delete a Vehicle
        public async Task DeleteVehicleAsync(int vehicleId)
        {
            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle ID is required for deletion.");

            await _vehicleRepository.DeleteAsync(vehicleId);
        }

        // Validate Vehicle Properties
        private void ValidateVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.Brand))
                throw new ArgumentException("Vehicle Brand is required.");

            if (string.IsNullOrWhiteSpace(vehicle.Model))
                throw new ArgumentException("Vehicle Model is required.");

            if (string.IsNullOrWhiteSpace(vehicle.Type))
                throw new ArgumentException("Vehicle Type is required.");

            if (vehicle.RentalPrice <= 0)
                throw new ArgumentException("Vehicle Rental Price must be a positive value.");

            if (vehicle.Mileage < 0)
                throw new ArgumentException("Vehicle Mileage cannot be negative.");

            if (!Enum.IsDefined(typeof(VehicleAvailabilityStatus), vehicle.VehicleAvailabilityStatus))
                throw new ArgumentException("Invalid Vehicle Availability Status.");

            if (vehicle.LocationId <= 0)
                throw new ArgumentException("Location ID must be valid.");
        }
    }
}
