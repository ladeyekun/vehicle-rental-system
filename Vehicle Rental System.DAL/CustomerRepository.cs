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
        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers
                .Include(c => c.Reservations)
                .Include(c => c.Histories)
                .ToListAsync();
        }

        // Get
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Reservations)
                .Include(c => c.Histories)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        // Add
        public async Task AddAsync(Customer customer, List<int> reservationIds, List<int> historyIds)
        {
            customer.Reservations = new List<Reservation>();
            customer.Histories = new List<History>();

            foreach (int resId in reservationIds)
            {
                Reservation reservation = await _context.Reservations.FindAsync(resId);
                if (reservation != null)
                {
                    customer.Reservations.Add(reservation);
                }
            }
            foreach (int histId in historyIds)
            {
                History history = await _context.Histories.FindAsync(histId);
                if (history != null)
                {
                    customer.Histories.Add(history);
                }
            }
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }


        // Update
        public async Task UpdateAsync(Customer customer, List<int> reservationIds, List<int> historyIds)
        {
            Customer existingCustomer = await _context.Customers
                .Include(c => c.Reservations)
                .Include(c => c.Histories)
                .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer Not Found");
            }

            // Update customer properties
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.CustomerGender = customer.CustomerGender;
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.DriversLicenseId = customer.DriversLicenseId;

            var newReservations = await _context.Reservations
                .Where(r => reservationIds.Contains(r.ReservationId))
                .ToListAsync();
            existingCustomer.Reservations = newReservations;

            var newHistories = await _context.Histories
                .Where(h => historyIds.Contains(h.RentalHistoryId))
                .ToListAsync();
            existingCustomer.Histories = newHistories;

            await _context.SaveChangesAsync();
        }



        // Delete 
        public async Task DeleteAsync(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<List<History>> GetHistoriesAsync()
        {
            return await _context.Histories.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<History> GetHistoryByIdAsync(int id)
        {
            return await _context.Histories.FindAsync(id);
        }
    }
}
