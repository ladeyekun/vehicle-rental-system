using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class ReviewService {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository repository) {
            _reviewRepository = repository;
        }
        public List<Review> GetReviews() {
            return _reviewRepository.GetReviews();
        }

        public Review GetReview(int id) {
            return _reviewRepository.GetReview(id);
        }

        public void AddReview(Review review) {
            _reviewRepository.AddReview(review);
        }
        public void UpdateReview(Review review) {
            _reviewRepository.UpdateReview(review);
        }
        public void DeleteReview(int id) {
            _reviewRepository.DeleteReview(id); 
        }

    }
}
