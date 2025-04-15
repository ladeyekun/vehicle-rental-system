using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class CustomerRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public CustomerRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Customer> GetCustomers() {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id) {
            return _context.Customers.Find(id);
        }

        public void AddCustomer(Customer customer) {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void UpdateCustomer(Customer customer) {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }
        public void DeleteCustomer(int id) {
            Customer customer = _context.Customers.Find(id);
            if (customer != null) {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
