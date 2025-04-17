using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly ReservationService _reservationService;
        private readonly HistoryService _historyService;

        public CustomerController(
            CustomerService customerService,
            ReservationService reservationService,
            HistoryService historyService)
        {
            _customerService = customerService;
            _reservationService = reservationService;
            _historyService = historyService;
        }

        public async Task<IActionResult> Index()
        {
            List<Customer> customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return NotFound();

            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            Customer customer = new Customer
            {
                CustomerName = model.CustomerName,
                Email = model.Email,
                Phone = model.Phone,
                CustomerGender = model.CustomerGender,
                DateOfBirth = model.DateOfBirth,
                DriversLicenseId = model.DriversLicenseId
            };

            await _customerService.AddCustomerAsync(customer, model.SelectedReservationIds, model.SelectedHistoryIds);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            CustomerViewModel viewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                Phone = customer.Phone,
                CustomerGender = customer.CustomerGender,
                DateOfBirth = customer.DateOfBirth,
                DriversLicenseId = customer.DriversLicenseId,
                SelectedReservationIds = customer.Reservations?.Select(r => r.ReservationId).ToList() ?? new(),
                SelectedHistoryIds = customer.Histories?.Select(h => h.RentalHistoryId).ToList() ?? new()
            };

            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerViewModel model)
        {
            if (id != model.CustomerId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View(model);
            }

            Customer customer = new Customer
            {
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                Email = model.Email,
                Phone = model.Phone,
                CustomerGender = model.CustomerGender,
                DateOfBirth = model.DateOfBirth,
                DriversLicenseId = model.DriversLicenseId
            };

            await _customerService.UpdateCustomerAsync(customer, model.SelectedReservationIds, model.SelectedHistoryIds);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");
        }

        private async Task PopulateDropdownsAsync(CustomerViewModel model)
        {
            var reservations = await _reservationService.GetReservations();
            var histories = await _historyService.GetAllHistoriesAsync();

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
