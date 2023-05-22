namespace Admin.Models
{
    public class OrderItems
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Products Product { get; set; }
    }
}
