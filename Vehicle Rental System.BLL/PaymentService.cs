using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class PaymentService {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository) {
            _paymentRepository = paymentRepository;
        }

        // Get all payments
        public async Task<List<Payment>> GetAllPaymentsAsync() {
            return await _paymentRepository.GetPaymentsAsync();
        }

        // Get a specific payment by ID
        public async Task<Payment?> GetPaymentByIdAsync(int id) {
            return await _paymentRepository.GetPaymentAsync(id);
        }

        // Add a new payment
        public async Task AddPaymentAsync(Payment payment) {
            if (payment.Amount < 0) {
                throw new Exception("Amount cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(payment.PaymentMethod)) {
                throw new Exception("Payment method is required.");
            }

            await _paymentRepository.AddPaymentAsync(payment);
        }

        // Update an existing payment
        public async Task UpdatePaymentAsync(Payment payment) {
            if (payment.Amount < 0) {
                throw new Exception("Amount cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(payment.PaymentMethod)) {
                throw new Exception("Payment method is required.");
            }

            await _paymentRepository.EditPaymentAsync(payment);
        }

        // Delete a payment by ID
        public async Task DeletePaymentAsync(int id) {
            await _paymentRepository.DeletePaymentAsync(id);
        }

    }
}
