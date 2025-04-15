using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepository.GetReviews();
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepository.GetReview(id);
        }

        public void CreateReview(Review review)
        {
            review.ReviewDate = DateTime.Now;
            _reviewRepository.AddReview(review);
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
        }

        public void DeleteReview(int id)
        {
            _reviewRepository.DeleteReview(id);
        }
    }
}
