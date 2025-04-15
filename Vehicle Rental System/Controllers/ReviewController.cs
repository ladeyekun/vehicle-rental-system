using Microsoft.AspNetCore.Mvc;
using Vehicle_Rental_System.BLL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: /Review
        public IActionResult Index()
        {
            var reviews = _reviewService.GetAllReviews();
            return View(reviews);
        }

        // GET: /Review/Details/5
        public IActionResult Details(int id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        // GET: /Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            _reviewService.CreateReview(review);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Review/Edit/5
        public IActionResult Edit(int id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        // POST: /Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            _reviewService.UpdateReview(review);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Review/Delete/5
        public IActionResult Delete(int id)
        {
            var review = _reviewService.GetReviewById(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        // POST: /Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _reviewService.DeleteReview(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
