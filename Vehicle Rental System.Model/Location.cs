namespace Vehicle_Rental_System.Model {
    public class Location {
        public int LocationId { get; set; }
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
