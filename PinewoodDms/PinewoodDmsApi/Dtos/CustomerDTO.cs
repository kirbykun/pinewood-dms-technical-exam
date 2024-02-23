using System.ComponentModel.DataAnnotations;

namespace PinewoodDmsApi.Dtos
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
    }
}
