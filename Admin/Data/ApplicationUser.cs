using Microsoft.AspNetCore.Identity;

namespace Admin.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
