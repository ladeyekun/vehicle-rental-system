using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicle_Rental_System.Model.ViewModel
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public double RentalPrice { get; set; }
        public VehicleAvailabilityStatus VehicleAvailabilityStatus { get; set; }
        public int Mileage { get; set; }
        public int SelectedLocationId { get; set; }
        public List<int> SelectedReservationIds { get; set; }
        public List<int> SelectedHistoryIds { get; set; }
        public List<SelectListItem> AvailableLocations { get; set; } = new();
        public List<SelectListItem> AvailableReservations { get; set; } = new();
        public List<SelectListItem> AvailableHistories { get; set; } = new();
    }
}
