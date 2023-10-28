namespace Admin.Models.ViewModels
{
    public class OrderDetailVM
    {
        public Order Order { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }
        public IEnumerable<CartDetail> CartDetail { get; set; }
    }
}
