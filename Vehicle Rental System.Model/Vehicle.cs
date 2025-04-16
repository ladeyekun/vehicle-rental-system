namespace Vehicle_Rental_System.Model {

    public enum VehicleAvailabilityStatus {
        Available,
        Unavailable
    }
    public class Vehicle {
        public int VehicleId { get; set; }
        public string Brand {  get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public double RentalPrice { get; set; }
        public VehicleAvailabilityStatus VehicleAvailabilityStatus { get; set; }
        public int Mileage { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public ICollection<History> Histories { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
