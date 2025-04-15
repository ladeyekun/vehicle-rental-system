namespace Vehicle_Rental_System.Model {
    public class Customer {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public Gender CustomerGender {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriversLicenseId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<History> Histories { get; set; }

        public enum Gender { Male, Female }
    }
}
