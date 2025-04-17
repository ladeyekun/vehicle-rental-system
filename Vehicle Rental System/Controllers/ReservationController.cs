using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;
using Vehicle_Rental_System.Model.ViewModel;

namespace Vehicle_Rental_System.Controllers {
    public class ReservationController : Controller {
        private readonly ReservationService _reservationService;
        private readonly CustomerService _customerService;
        private readonly VehicleService _vehicleService;
        private readonly LocationService _locationService;

        public ReservationController(ReservationService reservationService, CustomerService customerService, VehicleService vehicleService, LocationService locationService) {
            _reservationService = reservationService;
            _customerService = customerService;
            _vehicleService = vehicleService;
            _locationService = locationService;
        }

        [Authorize]
        public async Task<IActionResult> Index() {
            List<Reservation> reservations = await _reservationService.GetReservations();
            return View(reservations);
        }

        [Authorize(Roles = "Admin, Support")]
        [HttpGet]
        public async Task<IActionResult> Create() {
            ViewBag.Title = "Create Reservation";
            List<Customer> customers = await _customerService.GetAllCustomersAsync();
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            List<Location> locations = _locationService.GetAllLocations();

            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                Locations = locations.Select(l => new SelectListItem {
                    Value = l.LocationId.ToString(),
                    Text = l.Name
                }).ToList(),
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = new List<SelectListItem>()
            };
            return View(reservationViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetVehiclesByLocation(int locationId) {
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            var vehicleList = vehicles
                .Where(v => v.LocationId == locationId)
                .Select(v => new {
                    vehicleId = v.VehicleId,
                    name = v.Brand + " " + v.Model
                })
                .ToList();
            return Json(vehicleList);
        }

        [Authorize(Roles = "Admin, Support")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel model) {
            if (ModelState.IsValid) {
                Reservation reservation = new Reservation {
                    CustomerId = model.SelectedCustomerId,
                    VehicleId = model.SelectedVehicleId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Status = model.Status
                };
                _reservationService.AddReservation(reservation);
                 return RedirectToAction("Index");

            } else {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors) {
                    Console.WriteLine(error.ErrorMessage);
                }


                ViewBag.Title = "Create Reservation";
                List<Customer> customers = await _customerService.GetAllCustomersAsync();
                List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
                List<Location> locations = _locationService.GetAllLocations();

                ReservationViewModel reservationViewModel = new ReservationViewModel() {
                    Locations = locations.Select(l => new SelectListItem {
                        Value = l.LocationId.ToString(),
                        Text = l.Name
                    }).ToList(),
                    Customers = customers.Select(c => new SelectListItem {
                        Value = c.CustomerId.ToString(),
                        Text = c.CustomerName
                    }).ToList(),
                    Vehicles = new List<SelectListItem>()
                };
                return View(reservationViewModel);
            }
        }

        [Authorize]
        public async Task<IActionResult> Detail(int id) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }

            ViewBag.Title = "View Reservation Detail";
            return View(reservation);
        }

        [Authorize(Roles = "Admin, Support")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }
            ViewBag.Title = "Edit Reservation";
            List<Customer> customers = await _customerService.GetAllCustomersAsync();
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            List<Location> locations = _locationService.GetAllLocations();


            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                ReservationId = reservation.ReservationId,
                SelectedCustomerId = reservation.CustomerId,
                SelectedVehicleId = reservation.VehicleId,
                SelectedLocationId = reservation.Vehicle.LocationId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                Locations = locations.Select(l => new SelectListItem {
                    Value = l.LocationId.ToString(),
                    Text = l.Name
                }).ToList(),
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = vehicles.Where(v => v.LocationId == reservation.Vehicle.LocationId).Select(v => new SelectListItem {
                    Value = v.VehicleId.ToString(),
                    Text = v.Brand + " " + v.Model
                }).ToList()
            };
            return View(reservationViewModel);
        }


        [Authorize(Roles = "Admin, Support")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel model) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }
            if (id != model.ReservationId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                //reservation.ReservationId = id;
                reservation.CustomerId = model.SelectedCustomerId;
                reservation.VehicleId = model.SelectedVehicleId;
                reservation.StartDate = model.StartDate;
                reservation.EndDate = model.EndDate;
                reservation.Status = model.Status;
                await _reservationService.UpdateReservation(reservation);
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Edit Reservation";
            List<Customer> customers = await _customerService.GetAllCustomersAsync();
            List<Vehicle> vehicles = await _vehicleService.GetVehiclesAsync();
            List<Location> locations = _locationService.GetAllLocations();


            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                ReservationId = reservation.ReservationId,
                SelectedCustomerId = reservation.CustomerId,
                SelectedVehicleId = reservation.VehicleId,
                SelectedLocationId = reservation.Vehicle.LocationId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                Locations = locations.Select(l => new SelectListItem {
                    Value = l.LocationId.ToString(),
                    Text = l.Name
                }).ToList(),
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = vehicles.Where(v => v.LocationId == reservation.Vehicle.LocationId).Select(v => new SelectListItem {
                    Value = v.VehicleId.ToString(),
                    Text = v.Brand + " " + v.Model
                }).ToList()
            };
            return View(reservationViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }
            _reservationService.DeleteReservation(id);
            return RedirectToAction("Index");
        }
    }
}
