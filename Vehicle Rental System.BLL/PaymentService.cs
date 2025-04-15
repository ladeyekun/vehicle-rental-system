using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class PaymentService {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService(PaymentRepository paymentRepository) {
            _paymentRepository = paymentRepository;
        }
    }
        public List<Payment> GetPayments() {
            return _paymentRepository.GetPayments();
        }

        public Payment GetPayment(int id) {
            return _paymentRepository.GetPayment(id);
        }

        public void AddPayment(Payment payment) {
            _paymentRepository.AddPayment(payment);
        }
        public void UpdatePayment(Payment payment) {
            _paymentRepository.UpdatePayment(payment);
        }
        public void DeletePayment(int id) {
            _paymentRepository.DeletePayment(id);
        }
    }
}
