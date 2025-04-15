using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class PaymentRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public PaymentRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Payment> GetPayments() {
            return _context.Payments.ToList();
        }

        public Payment GetPayment(int id) {
            return _context.Payments.Find(id);
        }

        public void AddPayment(Payment payment) {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
        public void UpdatePayment(Payment payment) {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }
        public void DeletePayment(int id) {
            Payment payment = _context.Payments.Find(id);
            if (payment != null) {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }

    }
}
