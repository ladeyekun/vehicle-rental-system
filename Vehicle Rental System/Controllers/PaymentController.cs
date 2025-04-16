using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers {
    public class PaymentController : Controller {
        private readonly PaymentService _paymentService;
        private readonly ReservationService _reservationService;

        public PaymentController(PaymentService paymentService, ReservationService reservationService) {
            _paymentService = paymentService;
            _reservationService = reservationService;
        }

        //[Authorize]
        public async Task<IActionResult> Index() {
            List<Payment> payments = await _paymentService.GetAllPaymentsAsync();
            return View(payments);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create() {
            ViewBag.Reservations = await _reservationService.GetReservations();
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Payment payment) {
            if (ModelState.IsValid) {
                try {
                    await _paymentService.AddPaymentAsync(payment);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            ViewBag.Reservations = await _reservationService.GetReservations();
            return View(payment);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            Payment? payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) {
                return NotFound();
            }

            ViewBag.Reservations = await _reservationService.GetReservations();
            return View(payment);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Payment payment) {
            if (ModelState.IsValid) {
                try {
                    Payment? oldPayment = await _paymentService.GetPaymentByIdAsync(payment.PaymentId);
                    if (oldPayment == null) {
                        return NotFound();
                    }

                    // Update necessary fields
                    oldPayment.Amount = payment.Amount;
                    oldPayment.PaymentDate = payment.PaymentDate;
                    oldPayment.PaymentMethod = payment.PaymentMethod;
                    oldPayment.ReservationId = payment.ReservationId;

                    await _paymentService.UpdatePaymentAsync(oldPayment);

                    ViewBag.Reservations = await _reservationService.GetReservations();
                    ViewBag.SuccessMessage = "Payment updated successfully!";
                    return View(oldPayment);
                }
                catch (Exception ex) {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            ViewBag.Reservations = await _reservationService.GetReservations();
            return View(payment);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Payment/Details/{id}")]
        public async Task<IActionResult> Details(int id) {
            Payment? payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) {
                return NotFound();
            }
            return View(payment);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            Payment? payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) {
                return NotFound();
            }
            return View(payment);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Payment payment) {
            Payment? existing = await _paymentService.GetPaymentByIdAsync(payment.PaymentId);
            if (existing == null) {
                return NotFound();
            }

            await _paymentService.DeletePaymentAsync(payment.PaymentId);
            return RedirectToAction("Index");
        }

    }
}
