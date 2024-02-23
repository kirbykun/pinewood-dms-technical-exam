using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PinewoodDmsWeb.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = String.Empty;

        [Required]
        [EmailAddress(ErrorMessage="Email address is not valid")]
        public string Email { get; set; } = String.Empty;

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone must be numeric")]
        public string Phone { get; set; } = String.Empty;

        [DisplayName("Created Date")]
        public DateTime CreatedDttm { get; set; }

        [DisplayName("Updated Date")]
        public DateTime UpdatedDttm { get; set; }
    }
}
