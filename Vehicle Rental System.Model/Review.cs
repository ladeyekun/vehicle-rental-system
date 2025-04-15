namespace Vehicle_Rental_System.Model {
    public class Review {
        public int ReviewId { get; set; }
        public string Message { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
