using Admin.Models;
using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrder _order;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IOrder order, IHttpContextAccessor httpContextAccessor) // The constructor to bring in IOrder interface that enables getting information from the order data table
        {
            _order = order;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var orderVM = new OrderVM();
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;
            orderVM.Orders = await _order.GetOrdersByUserId(userId);
            orderVM.OrderItems = await _order.GetOrderItems();
            return View(orderVM);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var orderDetailVM = new OrderDetailVM();
            orderDetailVM.OrderItems = await _order.GetOrderItemsByOrderId(id);

            orderDetailVM.OrderItems.First().Order = await _order.GetOrderByOrderId(orderDetailVM.OrderItems.First().OrderID);
            return View(orderDetailVM);
        }
    }
}
