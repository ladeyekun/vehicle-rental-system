using Microsoft.EntityFrameworkCore;
using Vehicle_Rental_System.Model;

namespace Vehicle_Rental_System.DAL {
    public class ReservationRepository {
        private readonly VehicleRentalSystemDbContext _context;

        public ReservationRepository(VehicleRentalSystemDbContext context) {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservations() {
            return await _context.Reservations
                .Include(c => c.Customer)
                .Include(v => v.Vehicle)
                .Include(rv => rv.Review)
                .Include(p => p.Payment)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservation(int id) {
            return await _context.Reservations
                .Include(c => c.Customer)
                .Include(v => v.Vehicle)
                .Include(rv => rv.Review)
                .Include(p => p.Payment)
                .SingleOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task AddReservation(Reservation reservation) {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReservation(Reservation reservation) {
            Reservation existingReservation = await _context.Reservations.FindAsync(reservation.ReservationId);

            if (existingReservation  == null) {
                throw new Exception("Reservation not found");
            }
            existingReservation.CustomerId = reservation.CustomerId;
            existingReservation.VehicleId = reservation.VehicleId;
            existingReservation.StartDate = reservation.StartDate;
            existingReservation.EndDate = reservation.EndDate;
            existingReservation.Status = reservation.Status;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteReservation(int id) {
            Reservation existingReservation = await _context.Reservations.FindAsync(id);

            if (existingReservation == null) {
                throw new Exception("Reservation not found");
            }
            if (existingReservation != null) {
                _context.Reservations.Remove(existingReservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
