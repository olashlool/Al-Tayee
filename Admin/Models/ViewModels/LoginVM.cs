using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Admin.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "User Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
