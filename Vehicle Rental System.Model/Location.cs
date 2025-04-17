using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vehicle_Rental_System.Model {
    public class Location {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Location name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed {1} characters.")]
        public string Name { get; set; }

        [BindNever]
        [ValidateNever]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
