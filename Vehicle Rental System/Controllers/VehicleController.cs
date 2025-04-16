using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers
{
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;
        private readonly ReservationService _reservationService;
        private readonly HistoryService _historyService;
        private readonly LocationService _locationService;


        public VehicleController(VehicleService vehicleService, ReservationService reservationService, HistoryService historyService,
            LocationService locationService)
        {
            _vehicleService = vehicleService;
            _reservationService = reservationService;
            _historyService = historyService;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehicleService.AddVehicleAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _vehicleService.UpdateVehicleAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Vehicle vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            await _vehicleService.DeleteVehicleAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

