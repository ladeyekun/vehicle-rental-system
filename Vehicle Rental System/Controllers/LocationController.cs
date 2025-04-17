using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationService _locationService;
        private readonly VehicleService _vehicleService;

        public LocationController(LocationService locationService, VehicleService vehicleService)
        {
            _locationService = locationService;
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            var locations = _locationService.GetAllLocations();
            return View(locations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vehicles = await _vehicleService.GetVehiclesAsync();
            ViewBag.Vehicles = new SelectList(vehicles, "VehicleId", "Brand");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Location location, int? selectedVehicleId)
        {
            if (!ModelState.IsValid)
            {
                var vehicles = await _vehicleService.GetVehiclesAsync();
                ViewBag.Vehicles = new SelectList(vehicles, "VehicleId", "Brand", selectedVehicleId);
                return View(location);
            }

            _locationService.CreateLocation(location);

            if (selectedVehicleId.HasValue)
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(selectedVehicleId.Value);
                if (vehicle != null)
                {
                    vehicle.LocationId = location.LocationId;
                    await _vehicleService.UpdateVehicleAsync(vehicle);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null) return NotFound();
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location location)
        {
            if (!ModelState.IsValid) return View(location);
            _locationService.UpdateLocation(location);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null) return NotFound();
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _locationService.DeleteLocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
