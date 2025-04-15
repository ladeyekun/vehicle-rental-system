using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;
using Vehicle_Rental_System.DAL;

namespace Vehicle_Rental_System.BLL {
    public class HistoryService {
        private readonly HistoryRepository _historyRepository;

        public HistoryService(HistoryRepository historyRepository) {
            _historyRepository = historyRepository;
        }
        public List<History> GetHistories() {
            return _historyRepository.GetHistories();
        }

        public History GetHistory(int id) {
            return _historyRepository.GetHistory(id);
        }

        public void AddHistory(History history) {
            _historyRepository.AddHistory(history);
        }
        public void UpdateHistory(History history) {
            _historyRepository.UpdateHistory(history);
        }
        public void DeleteHistory(int id) {
            _historyRepository.DeleteHistory(id);
        }

    }
}
