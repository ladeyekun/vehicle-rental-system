using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;
using Vehicle_Rental_System.DAL;

namespace Vehicle_Rental_System.BLL {
    public class HistoryService {
        private readonly HistoryRepository _repository;

        public HistoryService(HistoryRepository repository) {
            _repository = repository;
        }

        // List all histories
        public async Task<List<History>> GetAllHistoriesAsync() {
            return await _repository.GetHistoriesAsync();
        }

        // Add a new history
        public async Task AddHistoryAsync(History history) {
            if (history.StartDate >= history.EndDate) {
                throw new Exception("Start date must be earlier than end date.");
            }

            await _repository.AddHistoryAsync(history);
        }

        // Edit an existing history
        public async Task UpdateHistoryAsync(History history) {
            if (history.StartDate >= history.EndDate) {
                throw new Exception("Start date must be earlier than end date.");
            }
            await _repository.EditHistoryAsync(history);
        }

        // Get a specific history record by ID
        public async Task<History?> GetHistoryByIdAsync(int id) {
            return await _repository.GetHistoryAsync(id);
        }

        // Delete a history record by ID
        public async Task DeleteHistoryAsync(int id) {
            await _repository.DeleteHistoryAsync(id);
        }
    }
}
