namespace Admin.Models.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
    }
}
