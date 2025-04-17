using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rental_System.Model {
    public class Review {
        public int ReviewId { get; set; }
        public string Message { get; set; }
        [Required]
        public int ReservationId { get; set; }

        [BindNever]
        [ValidateNever]
        public Reservation Reservation { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
