using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vehicle_Rental_System.BLL
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetReviewsAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetReviewAsync(id);
        }

        public async Task CreateReviewAsync(Review review)
        {
            review.ReviewDate = DateTime.Now;
            await _reviewRepository.AddReviewAsync(review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
        }
    }
}
