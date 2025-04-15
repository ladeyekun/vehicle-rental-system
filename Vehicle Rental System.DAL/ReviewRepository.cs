using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class ReviewRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public ReviewRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Review> GetReviews() {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int id) {
            return _context.Reviews.Find(id);
        }

        public void AddReview(Review review) {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        public void UpdateReview(Review review) {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }
        public void DeleteReview(int id) {
            Review review = _context.Reviews.Find(id);
            if (review != null) {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
    }
}
