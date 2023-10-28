namespace Admin.Models.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<Order> Orders { get; set; }
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>(); // Initialize as an empty list
    }
}
