using System.ComponentModel.DataAnnotations;

namespace Admin.Models.ViewModels
{
    public class RegisterVM
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
