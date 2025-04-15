using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class ReservationRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public ReservationRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public List<Reservation> GetReservations() {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservation(int id) {
            return _context.Reservations.Find(id);
        }

        public void AddReservation(Reservation reservation) {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
        public void UpdateReservation(Reservation reservation) {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }
        public void DeleteReservation(int id) {
            Reservation reservation = _context.Reservations.Find(id);
            if (reservation != null) {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }
    }
}
