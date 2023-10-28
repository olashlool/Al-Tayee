using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class OrderServices : IOrder
    {
        private readonly AltayeeDBContext _context;

        public OrderServices(AltayeeDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return order;
        }
        public async Task<Order> EditOrder(Guid id , Order order)
        {
            // Retrieve the existing order from the database based on the ID
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                // Handle the case where the order with the specified ID is not found.
                throw new Exception("Order not found.");
            }

            // Update the PaymentStatus property of the existing order with the new value
            existingOrder.PaymentStstus = order.PaymentStstus;
            existingOrder.OrderStatus = existingOrder.PaymentStstus == "PAID" ? "Completed" : "Pending";

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return the updated order
            return existingOrder;

        }
        public async Task CreateOrderItems(IEnumerable<OrderItems> orderItems)
        {
            _context.OrderItems.AddRange(orderItems);
            await _context.SaveChangesAsync();
        }
        public async Task<OrderItems> CreateOrderItem(OrderItems orderItem)
        {
            _context.Entry(orderItem).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return orderItem;
        }
        public async Task<Order> GetLatestOrderForUser(string userId)
        {
            return await _context.Orders.AsNoTracking()
                .Where(order => order.UserID == userId)
                .OrderByDescending(order => order.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderByOrderId(Guid Id)
        {
            return await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == Id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders.AsNoTracking()
                .Where(order => order.UserID == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IList<OrderItems>> GetOrderItemsByOrderId(Guid orderId)
        {
            return await _context.OrderItems.AsNoTracking()
                .Where(orderItems => orderItems.OrderID == orderId)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<List<OrderItems>> GetOrderItems()
        {
            return await _context.OrderItems.AsNoTracking()
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<OrderItems> UpdateOrderItems(OrderItems orderItem)
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

        public async Task DeleteOrder(Guid id)
        {
            Order order = await GetOrderByOrderId(id);
            _context.Entry(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


    }
}
