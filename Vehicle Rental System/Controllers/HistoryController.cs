using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers {
    public class HistoryController : Controller {
        private readonly HistoryService _historyService;
        private readonly CustomerService _customerService;
        private readonly VehicleService _vehicleService;

        public HistoryController(HistoryService historyService, CustomerService customerService, VehicleService vehicleService) {
            _historyService = historyService;
            _customerService = customerService;
            _vehicleService = vehicleService;
        }

        //[Authorize]
        public async Task<IActionResult> Index() {
            List<History> histories = await _historyService.GetAllHistoriesAsync();
            return View(histories);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create() {
            ViewBag.Customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Vehicles = await _vehicleService.GetVehiclesAsync();
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(History history) {
            if (ModelState.IsValid) {
                try {
                    await _historyService.AddHistoryAsync(history);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
<<<<<<< HEAD
            ViewBag.Customers = _customerService.GetCustomers();
            ViewBag.Vehicles = _vehicleService.GetVehiclesAsync();
=======
            ViewBag.Customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Vehicles = await _vehicleService.GetVehiclesAsync();
>>>>>>> 58ab406f19c916b7a61101d931a7ab7bf017a73b
            return View(history);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            History? history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null) {
                return NotFound();
            }
<<<<<<< HEAD
            ViewBag.Users = _customerService.GetCustomers();
            ViewBag.Vehicles = _vehicleService.GetVehiclesAsync();
=======
            ViewBag.Users = await _customerService.GetAllCustomersAsync();
            ViewBag.Vehicles = await _vehicleService.GetVehiclesAsync();
>>>>>>> 58ab406f19c916b7a61101d931a7ab7bf017a73b
            return View(history);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(History history) {
            if (ModelState.IsValid) {
                try {
                    History? oldHistory = await _historyService.GetHistoryByIdAsync(history.RentalHistoryId);
                    if (oldHistory == null) {
                        return NotFound();
                    }

                    // Update only necessary fields
                    oldHistory.StartDate = history.StartDate;
                    oldHistory.EndDate = history.EndDate;
                    oldHistory.CustomerId = history.CustomerId;
                    oldHistory.VehicleId = history.VehicleId;

                    await _historyService.UpdateHistoryAsync(oldHistory);

<<<<<<< HEAD
                    ViewBag.Customers = _customerService.GetCustomers();
                    ViewBag.Vehicles = _vehicleService.GetVehiclesAsync();
=======
                    ViewBag.Customers = await _customerService.GetAllCustomersAsync();
                    ViewBag.Vehicles = await _vehicleService.GetVehiclesAsync();
>>>>>>> 58ab406f19c916b7a61101d931a7ab7bf017a73b
                    ViewBag.SuccessMessage = "History updated successfully!";
                    return View(oldHistory);
                }
                catch (Exception ex) {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

<<<<<<< HEAD
            ViewBag.Customers = _customerService.GetCustomers();
            ViewBag.Vehicles = _vehicleService.GetVehiclesAsync();
=======
            ViewBag.Customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Vehicles = await _vehicleService.GetVehiclesAsync();
>>>>>>> 58ab406f19c916b7a61101d931a7ab7bf017a73b
            return View(history);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("History/Details/{id}")]
        public async Task<IActionResult> Details(int id) {
            History? history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null) {
                return NotFound();
            }
            return View(history);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            History? history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null) {
                return NotFound();
            }
            return View(history);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(History history) {
            History? existing = await _historyService.GetHistoryByIdAsync(history.RentalHistoryId);
            if (existing == null) {
                return NotFound();
            }

            await _historyService.DeleteHistoryAsync(history.RentalHistoryId);
            return RedirectToAction("Index");
        }

    }

}
