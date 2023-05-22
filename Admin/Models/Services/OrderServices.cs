using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class OrderServices : IOrder
    {
        // Establishes a private connection to a database via dependency injection
        private readonly AltayeeDBContext _context;

        public OrderServices(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order order) // Create an order data by saving an Order object into the connected database
        {
            _context.Entry(order).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return order;
        }
        public async Task<OrderItems> CreateOrderItem(OrderItems orderItem) // Create an orderItems data by saving an orderItems object into the connected database
        {
            _context.Entry(orderItem).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return orderItem;
        }
        public async Task<Order> GetLatestOrderForUser(string userId) // Gets the latest order data from the connencted database
        {
            var orders = await GetOrdersByUserId(userId);
            return orders.OrderByDescending(order => order.ID).FirstOrDefault();
        }
        public async Task<Order> GetOrderByOrderId(Guid Id) // Gets the latest order data from the connencted database
        {
            return _context.Orders.FirstOrDefault(x => x.ID == Id);
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId) // Gets all of the oder data for a user from the connencted database
        {
            return await _context.Orders.Where(order => order.UserID == userId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders() // Gets all of the oder from the connencted database
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<IList<OrderItems>> GetOrderItemsByOrderId(Guid orderId) // Gets all of the oderItems data for an orderId from the connencted database
        {
            return await _context.OrderItems.Where(orderItems => orderItems.OrderID == orderId).Include(x => x.Product).ToListAsync();
        }

        public async Task<IEnumerable<OrderItems>> GetOrderItems() // Gets all of the oderItems from the connencted database
        {
            return await _context.OrderItems.Include(x => x.Product).ToListAsync();
        }
        public async Task<OrderItems> UpdateOrderItems(OrderItems orderItem) // Update an orderItem data from the connected database
        {
            var updateOrderItem = new OrderItems
            {
                OrderID = orderItem.ID,
                ProductID = orderItem.ProductID,
            };
            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateOrderItem;
        }
        public async Task DeleteOrder(Guid id) // Deletes a Brands data based on the id from the connected database
        {
            Order brands = await GetOrderByOrderId(id);
            _context.Entry(brands).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
