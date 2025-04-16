namespace Vehicle_Rental_System.Model {
    public class History {
        public int RentalHistoryId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCost { get; set; }
    }
}