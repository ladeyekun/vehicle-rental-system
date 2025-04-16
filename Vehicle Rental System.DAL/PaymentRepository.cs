using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class PaymentRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public PaymentRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        // Get all payments with reservation info
        public async Task<List<Payment>> GetPaymentsAsync() {
            return await _context.Payments
                .Include(p => p.Reservation)
                .ThenInclude(r => r.Customer) // Optional, only if you need Customer info in views
                .ToListAsync();
        }

        // Get a specific payment by ID
        public async Task<Payment?> GetPaymentAsync(int id) {
            return await _context.Payments
                .Include(p => p.Reservation)
                .ThenInclude(r => r.Customer)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        // Add a new payment
        public async Task AddPaymentAsync(Payment payment) {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        // Update an existing payment
        public async Task EditPaymentAsync(Payment payment) {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        // Delete a payment by ID
        public async Task DeletePaymentAsync(int id) {
            Payment? payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
            if (payment != null) {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
