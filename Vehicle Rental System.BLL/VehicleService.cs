using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class VehicleService {
        private readonly VehicleRepository _vehicleRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly HistoryRepository _historyRepository;
        private readonly LocationRepository _locationRepository;


        public VehicleService(VehicleRepository vehicleRepository, ReservationRepository reservationRepository, HistoryRepository historyRepository,
            LocationRepository locationRepository) {
            _vehicleRepository = vehicleRepository;
            _reservationRepository = reservationRepository;
            _historyRepository = historyRepository;
            _locationRepository = locationRepository;
        }

        // Get All Vehicle
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _vehicleRepository.GetVehicles();
        }

        // Get Vehicle By Id

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new ArgumentException("Please Enter Valid Vehicle Id.", nameof(vehicleId));
            }
            Vehicle vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException($"Unfortunately, Vehicle With ID {vehicleId} Not Found.");
            }
            return vehicle;
        }

        // Add New Vehicle

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.AddAsync(vehicle);
        }

        // Update Current Vehicle Info

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        // Delete Vehicle Info
        public async Task DeleteVehicleAsync(int vehicleId)
        {
            await _vehicleRepository.DeleteAsync(vehicleId);
        }
    }
}
