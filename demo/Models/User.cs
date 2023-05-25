using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class User
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        ErrorMessage = "Please enter valid phone number")]
        public string? Phone { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "StreetAddress must below  150 characters")]

        public string? StreetAddress { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]

        public string? Password { get; set; }


    }
}
