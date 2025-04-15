namespace Vehicle_Rental_System.Model {
    public class Payment {
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

    }
}
