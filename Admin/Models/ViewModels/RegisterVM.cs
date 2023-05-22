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
        [Required]
        public string Gender { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
