using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _order;

        public OrderController(IOrder order) // The constructor to bring in IOrder interface that enables getting information from the order data table
        {
            _order = order;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = new OrderVM();
            orders.Orders = await _order.GetOrders();
            foreach (var item in orders.Orders)
            {
                orders.OrderItems = await _order.GetOrderItemsByOrderId(item.ID);
            }
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var orderDetailVM = new OrderDetailVM();
            orderDetailVM.OrderItems = await _order.GetOrderItemsByOrderId(id);

            orderDetailVM.OrderItems.First().Order = await _order.GetOrderByOrderId(orderDetailVM.OrderItems.First().OrderID);
            return View(orderDetailVM);
        }
    }
}
