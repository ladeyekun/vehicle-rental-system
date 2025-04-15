using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class CustomerService {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }
        public List<Customer> GetCustomers() {
            return _customerRepository.GetCustomers();
        }

        public Customer GetCustomer(int id) {
            return _customerRepository.GetCustomer(id);
        }

        public void AddCustomer(Customer customer) {
            _customerRepository.AddCustomer(customer);
        }
        public void UpdateCustomer(Customer customer) {
            _customerRepository.UpdateCustomer(customer);
        }
        public void DeleteCustomer(int id) {
            _customerRepository.DeleteCustomer(id);
        }

    }
}
