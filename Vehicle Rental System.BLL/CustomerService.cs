using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class CustomerService
    {

        private readonly CustomerRepository _customerRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly HistoryRepository _historyRepository;

        public CustomerService(
            CustomerRepository customerRepository,
            LocationRepository locationRepository,
            ReservationRepository reservationRepository,
            HistoryRepository historyRepository)
        {
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
            _reservationRepository = reservationRepository;
            _historyRepository = historyRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer must have a valid ID.");

            Customer customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new ArgumentException("Customer not found.");

            return customer;
        }

        public async Task AddCustomerAsync(Customer customer, List<int> reservationIds, List<int> historyIds)
        {
            ValidateCustomer(customer);
            await _customerRepository.AddAsync(customer, reservationIds, historyIds);
        }

        public async Task UpdateCustomerAsync(Customer customer, List<int> reservationIds, List<int> historyIds)
        {
            if (customer.CustomerId <= 0)
                throw new ArgumentException("Customer ID is required for update.");

            ValidateCustomer(customer);
            await _customerRepository.UpdateAsync(customer, reservationIds, historyIds);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
        }

        private void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
                throw new ArgumentException("Customer Name Is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone number is required.");

            if (!Enum.IsDefined(typeof(Customer.Gender), customer.CustomerGender))
                throw new ArgumentException("Invalid gender.");

            if (string.IsNullOrWhiteSpace(customer.DriversLicenseId))
                throw new ArgumentException("Driver's license ID is required.");

            if (customer.DateOfBirth > DateTime.Today)
                throw new ArgumentException("Date of birth cannot be in the future.");
        }

    }
}
