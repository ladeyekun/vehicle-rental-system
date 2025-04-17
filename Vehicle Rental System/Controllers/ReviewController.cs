using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly ReservationService _reservationService;

        public ReviewController(ReviewService reviewService, ReservationService reservationService)
        {
            _reviewService = reviewService;
            _reservationService = reservationService;
        }

        // GET: /Review
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        // GET: /Review/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null) return NotFound();
            return View(review);
        }

        private async Task PopulateReservationsAsync(int? selectedId = null)
        {
            var list = await _reservationService.GetReservations();
            ViewBag.Reservations = list
                .Select(r => new SelectListItem
                {
                    Value = r.ReservationId.ToString(),
                    Text = $"Reservation #{r.ReservationId} - {r.Customer?.CustomerName}",
                    Selected = (r.ReservationId == selectedId)
                })
                .ToList();
        }

        // GET: /Review/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateReservationsAsync();
            return View(new Review());
        }

        // POST: /Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                await PopulateReservationsAsync(review.ReservationId);
                return View(review);
            }

            review.ReviewDate = DateTime.Now;
            await _reviewService.CreateReviewAsync(review);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Review/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null) return NotFound();

            await PopulateReservationsAsync(review.ReservationId);
            return View(review);
        }

        // POST: /Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (id != review.ReviewId) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateReservationsAsync(review.ReservationId);
                return View(review);
            }

            await _reviewService.UpdateReviewAsync(review);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Review/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null) return NotFound();
            return View(review);
        }

        // POST: /Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
