using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class ReservationService {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService(ReservationRepository reservationRepository) {
            _reservationRepository = reservationRepository;
        }
        public async Task<List<Reservation>> GetReservations() {
            return await _reservationRepository.GetReservations();
        }

        public async Task<Reservation> GetReservation(int id) {
            return await _reservationRepository.GetReservation(id);
        }

        public async Task AddReservation(Reservation reservation) {
            await _reservationRepository.AddReservation(reservation);
        }
        public async Task UpdateReservation(Reservation reservation) {
            await _reservationRepository.UpdateReservation(reservation);
        }
        public async Task DeleteReservation(int id) {
            await _reservationRepository.DeleteReservation(id);
        }

    }
}
