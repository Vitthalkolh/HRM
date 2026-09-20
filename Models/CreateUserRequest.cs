using System.ComponentModel.DataAnnotations;

namespace HRM.API.Models
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? CompanyLeavingDate { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
    }
}