using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly ReservationService _reservationService;
        private readonly HistoryService _historyService;

        public CustomerController(CustomerService customerService, ReservationService reservationService,
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

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            List<Reservation> reservations = await _reservationService.GetReservations();
            List<History> histories = await _historyService.GetAllHistoriesAsync();

            ViewBag.Customer = customer;
            ViewBag.Reservations = reservations;
            ViewBag.Histories = histories;

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
