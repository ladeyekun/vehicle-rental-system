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

        public CustomerService(CustomerRepository customerRepository, LocationRepository locationRepository, ReservationRepository
            reservationRepository, HistoryRepository historyRepository)
        {
            _customerRepository = customerRepository;
            _locationRepository = locationRepository;
            _reservationRepository = reservationRepository;
            _historyRepository = historyRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            List<Customer> customers = await _customerRepository.GetCustomers();
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer Must Have An Id");
            }
            Customer customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new ArgumentException("Unfortunately, Customer Not Found");
            }
            return customer;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (customer.CustomerId <= 0)
            {
                throw new ArgumentException("Customer Id Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                throw new ArgumentException("Customer Name Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Email Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                throw new ArgumentException("Phone Number Is Required");
            }
            if (!Enum.IsDefined(typeof(Customer.Gender), customer.CustomerGender))
            {
                throw new ArgumentException("Invalid Gender.");
            }
            if (string.IsNullOrWhiteSpace(customer.DriversLicenseId))
            {
                throw new ArgumentException("Driver's license ID Is Required.");
            }
            if (customer.DateOfBirth > DateTime.Today)
            {
                throw new ArgumentException("Date Of Birth Cannot Be In The Future.");
            }
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (customer.CustomerId <= 0)
            {
                throw new ArgumentException("Customer ID Is Required To Update");
            }
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                throw new ArgumentException("Customer Name Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Email Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                throw new ArgumentException("Phone Number Is Required");
            }
            if (string.IsNullOrWhiteSpace(customer.DriversLicenseId))
            {
                throw new ArgumentException("Driver's license ID Is Required");
            }
            if (customer.DateOfBirth > DateTime.Today)
            {
                throw new ArgumentException("Date Of Birth Cannot Be In The Future");
            }
            if (!Enum.IsDefined(typeof(Customer.Gender), customer.CustomerGender))
            {
                throw new ArgumentException("Invalid Gender");
            }
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
        }

    }
}
