using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicle_Rental_System.Model.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Customer.Gender CustomerGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriversLicenseId { get; set; }
        public List<int> SelectedReservationIds { get; set; }
        public List<int> SelectedHistoryIds { get; set; }
        public List<SelectListItem> AvailableReservations { get; set; }
        public List<SelectListItem> AvailableHistories { get; set; }
    }
}
