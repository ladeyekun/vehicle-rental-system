using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class HistoryRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public HistoryRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<History> GetHistories() {
            return _context.Histories.ToList();
        }

        public History GetHistory(int id) {
            return _context.Histories.Find(id);
        }

        public void AddHistory(History history) {
            _context.Histories.Add(history);
            _context.SaveChanges();
        }
        public void UpdateHistory(History history) {
            _context.Histories.Update(history);
            _context.SaveChanges();
        }
        public void DeleteHistory(int id) {
            History history = _context.Histories.Find(id);
            if (history != null) {
                _context.Histories.Remove(history);
                _context.SaveChanges();
            }
        }
    }
}
