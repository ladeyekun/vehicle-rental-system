namespace Vehicle_Rental_System.Model {
    public class Reservation {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int VehicleId {  get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Review Review { get; set; }
        public Payment Payment { get; set; }
        public ReservationStatus Status { get; set; }
        public enum ReservationStatus { Booked, Completed, Canceled }
    }
}
