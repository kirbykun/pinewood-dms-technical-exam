using System.ComponentModel.DataAnnotations;

namespace PinewoodDmsApi.Dtos
{
    public class InsertCustomerDTO
    {
        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string LastName { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = String.Empty;
    }
}
