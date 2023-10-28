namespace Admin.Models.Interface
{
    public interface IOrder
    {
        public Task<Order> CreateOrder(Order order);
        public Task<Order> EditOrder(Guid id,Order order);
        public Task<OrderItems> CreateOrderItem(OrderItems orderItem);
        public Task<Order> GetLatestOrderForUser(string userId);
        public Task<Order> GetOrderByOrderId(Guid id);
        public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        public Task<IEnumerable<Order>> GetOrders();
        public Task<IList<OrderItems>> GetOrderItemsByOrderId(Guid orderId);
        public Task<List<OrderItems>> GetOrderItems();
        public Task<OrderItems> UpdateOrderItems(OrderItems orderItems);
        public Task DeleteOrder(Guid id);
        public Task CreateOrderItems(IEnumerable<OrderItems> orderItems);

    }
}
