using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class CustomerRepository {

        private readonly VehicleRentalSystemDbContext _context;

        public CustomerRepository(VehicleRentalSystemDbContext context)
        {
            _context = context;
        }

        // Get All Customer

        public async Task<List<Customer>> GetAllAsync()
        {
            List<Customer> customers = await _context.Customers
                .Include(c => c.Reservations)
                .Include(c => c.Histories)
                .ToListAsync();

            return customers;
        }

        // Get Customer By Id

        public async Task<Customer> GetByIdAsync(int id)
        {
            Customer customer = await _context.Customers
                .Include(c => c.Reservations)
                .Include(c => c.Histories)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            return customer;
        }

        // Add New Customer

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        // Update Cuntomer Info

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        // Delete Customer

        public async Task DeleteAsync(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
