using System.ComponentModel.DataAnnotations;

namespace WeddingRegistration.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public String? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        public String? LastName { get; set; }

        [Required(ErrorMessage = "Please enter how many guests you are bringing.")]
        [Range(0, 20, ErrorMessage = "Number of guests must be positive.")]
        public int NumberOfGuests { get; set; }

        public List<String> Gifts { get; set; } = [];
        public int RegistrationId { get; set; } //TODO auto-increment

        // Slug if needed
        // public String Slug => $"{FirstName}-{LastName}";
        
    }
}
