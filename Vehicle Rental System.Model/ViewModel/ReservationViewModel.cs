using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicle_Rental_System.Model.ViewModel {
    public class ReservationViewModel {
        public int ReservationId { get; set; }
        public int SelectedCustomerId { get; set; }
        public List<SelectListItem>? Customers { get; set; }
        public int SelectedVehicleId { get; set; }
        public List<SelectListItem>? Vehicles { get; set; }
        public int SelectedLocationId { get; set; }
        public List<SelectListItem>? Locations { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Booked;
    }
}
