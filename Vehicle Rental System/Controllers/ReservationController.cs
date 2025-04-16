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

        public ReservationController(ReservationService reservationService, CustomerService customerService, VehicleService vehicleService) {
            _reservationService = reservationService;
            _customerService = customerService;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index() {
            List<Reservation> reservations = await _reservationService.GetReservations();
            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            ViewBag.Title = "Create Reservation";
            List<Customer> customers = await _customerService.GetCustomers();
            List<Vehicle> vehicles = await _vehicleService.GetVehicles();

            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = vehicles.Select(v => new SelectListItem {
                    Value = v.VehicleId.ToString(),
                    Text = v.Brand + " " + v.Model
                }).ToList()
            };
            return View(reservationViewModel);
        }


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
                List<Customer> customers = await _customerService.GetCustomers();
                List<Vehicle> vehicles = await _vehicleService.GetVehicles();

                ReservationViewModel reservationViewModel = new ReservationViewModel() {
                    Customers = customers.Select(c => new SelectListItem {
                        Value = c.CustomerId.ToString(),
                        Text = c.CustomerName
                    }).ToList(),
                    Vehicles = vehicles.Select(v => new SelectListItem {
                        Value = v.VehicleId.ToString(),
                        Text = v.Brand + " " + v.Model
                    }).ToList()
                };
                return View(reservationViewModel);
            }
        }

        public async Task<IActionResult> Detail(int id) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }

            ViewBag.Title = "View Reservation Detail";
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            Reservation reservation = await _reservationService.GetReservation(id);
            if (reservation == null) {
                return NotFound();
            }
            ViewBag.Title = "Edit Reservation";
            List<Customer> customers = await _customerService.GetCustomers();
            List<Vehicle> vehicles = await _vehicleService.GetVehicles();

            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                ReservationId = reservation.ReservationId,
                SelectedCustomerId = reservation.CustomerId,
                SelectedVehicleId = reservation.VehicleId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = reservation.Status,
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = vehicles.Select(v => new SelectListItem {
                    Value = v.VehicleId.ToString(),
                    Text = v.Brand + " " + v.Model
                }).ToList()
            };
            return View(reservationViewModel);
        }

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
            List<Customer> customers = await _customerService.GetCustomers();
            List<Vehicle> vehicles = await _vehicleService.GetVehicles();
            ReservationViewModel reservationViewModel = new ReservationViewModel() {
                ReservationId = id,
                SelectedCustomerId = model.SelectedCustomerId,
                SelectedVehicleId = model.SelectedVehicleId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                Customers = customers.Select(c => new SelectListItem {
                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName
                }).ToList(),
                Vehicles = vehicles.Select(v => new SelectListItem {
                    Value = v.VehicleId.ToString(),
                    Text = v.Brand + " " + v.Model
                }).ToList()
            };
            return View(reservationViewModel);
        }

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
