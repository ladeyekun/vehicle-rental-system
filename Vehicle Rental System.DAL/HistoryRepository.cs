using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class HistoryRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public HistoryRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        // List all history records
        public async Task<List<History>> GetHistoriesAsync() {
            return await _context.Histories.ToListAsync();
        }

        // Add a new history record
        public async Task AddHistoryAsync(History history) {
            await _context.Histories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        // Update an existing history record
        public async Task EditHistoryAsync(History history) {
            _context.Histories.Update(history);
            await _context.SaveChangesAsync();
        }

        // Get a specific history record by ID
        public async Task<History?> GetHistoryAsync(int id) {
            return await _context.Histories.FirstOrDefaultAsync(h => h.RentalHistoryId == id);
        }

        // Delete a history record by ID
        public async Task DeleteHistoryAsync(int id) {
            History? history = await _context.Histories.FirstOrDefaultAsync(h => h.RentalHistoryId == id);
            if (history != null) {
                _context.Histories.Remove(history);
                await _context.SaveChangesAsync();
            }
        }
    }
}
