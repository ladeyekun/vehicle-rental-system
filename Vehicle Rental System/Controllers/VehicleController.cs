using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;
using Vehicle_Rental_System.Model.ViewModel;

namespace Vehicle_Rental_System.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly ReservationService _reservationService;
        private readonly HistoryService _historyService;
        private readonly LocationService _locationService;

        public VehicleController(
            VehicleService vehicleService,
            ReservationService reservationService,
            HistoryService historyService,
            LocationService locationService)
        {
            _vehicleService = vehicleService;
            _reservationService = reservationService;
            _historyService = historyService;
            _locationService = locationService;
        }

        // Index

        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            return View(vehicles);
        }

        // Details

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return NotFound();

            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            VehicleViewModel viewModel = new VehicleViewModel();
            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }

        // Create

        [HttpPost]
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            Vehicle vehicle = new Vehicle
            {
                Brand = model.Brand,
                Model = model.Model,
                Type = model.Type,
                RentalPrice = model.RentalPrice,
                VehicleAvailabilityStatus = model.VehicleAvailabilityStatus,
                Mileage = model.Mileage,
                LocationId = model.SelectedLocationId
            };

            await _vehicleService.AddVehicleAsync(vehicle, model.SelectedReservationIds, model.SelectedHistoryIds);
            return RedirectToAction("Index");
        }

        // Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            VehicleViewModel viewModel = new VehicleViewModel
            {
                VehicleId = vehicle.VehicleId,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Type = vehicle.Type,
                RentalPrice = vehicle.RentalPrice,
                VehicleAvailabilityStatus = vehicle.VehicleAvailabilityStatus,
                Mileage = vehicle.Mileage,
                SelectedLocationId = vehicle.LocationId,
                SelectedReservationIds = vehicle.Reservations?.Select(r => r.ReservationId).ToList() ?? new(),
                SelectedHistoryIds = vehicle.Histories?.Select(h => h.RentalHistoryId).ToList() ?? new()
            };

            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }

        // Edit

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleViewModel model)
        {
            if (id != model.VehicleId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            Vehicle vehicle = new Vehicle
            {
                VehicleId = model.VehicleId,
                Brand = model.Brand,
                Model = model.Model,
                Type = model.Type,
                RentalPrice = model.RentalPrice,
                VehicleAvailabilityStatus = model.VehicleAvailabilityStatus,
                Mileage = model.Mileage,
                LocationId = model.SelectedLocationId
            };

            await _vehicleService.UpdateVehicleAsync(vehicle, model.SelectedReservationIds, model.SelectedHistoryIds);
            return RedirectToAction("Index");
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // Delete

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteVehicleAsync(id);
            return RedirectToAction("Index");
        }
        private async Task PopulateDropdownsAsync(VehicleViewModel model)
        {
            var locations = _locationService.GetAllLocations();
            var reservations = await _reservationService.GetReservations();
            var histories = await _historyService.GetAllHistoriesAsync();

            model.AvailableLocations = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = l.Name
            }).ToList();

            model.AvailableReservations = reservations.Select(r => new SelectListItem
            {
                Value = r.ReservationId.ToString(),
                Text = $"Reservation #{r.ReservationId}"
            }).ToList();

            model.AvailableHistories = histories.Select(h => new SelectListItem
            {
                Value = h.RentalHistoryId.ToString(),
                Text = $"History #{h.RentalHistoryId}"
            }).ToList();
        }

    }
}

