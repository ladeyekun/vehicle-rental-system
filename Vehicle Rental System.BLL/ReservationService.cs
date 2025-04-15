using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.DAL;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.BLL {
    public class ReservationService {
        private readonly ReservationRepository _reservationRepository;

        public ReservationService(ReservationRepository reservationRepository) {
            _reservationRepository = reservationRepository;
        }
        public List<Reservation> GetReservations() {
            return _reservationRepository.GetReservations();
        }

        public Reservation GetReservation(int id) {
            return _reservationRepository.GetReservation(id);
        }

        public void AddReservation(Reservation reservation) {
            _reservationRepository.AddReservation(reservation);
        }
        public void UpdateReservation(Reservation reservation) {
            _reservationRepository.UpdateReservation(reservation);
        }
        public void DeleteReservation(int id) {
            _reservationRepository.DeleteReservation(id);
        }

    }
}
