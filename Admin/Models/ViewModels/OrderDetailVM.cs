namespace Admin.Models.ViewModels
{
    public class OrderDetailVM
    {
        public Order Order { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
    }
}
