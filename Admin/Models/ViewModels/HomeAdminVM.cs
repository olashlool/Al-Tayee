namespace Admin.Models.ViewModels
{
    public class HomeAdminVM
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ContactUs> ContactUs { get; set; }
        public IEnumerable<Products> Products { get; set; }
    }
}
