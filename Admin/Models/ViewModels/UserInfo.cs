namespace Admin.Models.ViewModels
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
