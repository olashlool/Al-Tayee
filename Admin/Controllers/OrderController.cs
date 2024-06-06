﻿using Admin.Models;
using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _order;
        private readonly ICartRepository _cartRepo;

        public OrderController(IOrder order, ICartRepository cartRepo) // The constructor to bring in IOrder interface that enables getting information from the order data table
        {
            _order = order;
            _cartRepo = cartRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = new OrderVM();
            orders.Orders = await _order.GetOrders();

            // Define the expected date format
            string[] formats = { "M/d/yyyy h:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt" }; // Add more formats if needed
            var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;

            orders.Orders = orders.Orders
                .OrderByDescending(o =>
                {
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(o.Timestamp.Trim(';'), formats, cultureInfo, System.Globalization.DateTimeStyles.None, out parsedDate))
                    {
                        return parsedDate;
                    }
                    return DateTime.MinValue; // Default value for invalid date strings
                })
                .ToList();

            foreach (var item in orders.Orders)
            {
                var orderItems = await _order.GetOrderItemsByOrderId(item.ID);
                orders.OrderItems.AddRange(orderItems); // Add the orderItems to the list
            }

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var orderDetailVM = new OrderDetailVM();
            orderDetailVM.OrderItems = await _order.GetOrderItemsByOrderId(id);

            orderDetailVM.OrderItems.First().Order = await _order.GetOrderByOrderId(orderDetailVM.OrderItems.First().OrderID);

            return View(orderDetailVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDetailVM orderDetailVM ,Guid id) 
        {
            var orderItems = new Order();
            var order = await _order.GetOrderByOrderId(id);
            orderItems = await _order.EditOrder(id , orderDetailVM.Order);

            return RedirectToAction("Detail", new { id });
        }
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var orderDetailVM = new OrderDetailVM();
            orderDetailVM.OrderItems = await _order.GetOrderItemsByOrderId(id);

            orderDetailVM.OrderItems.First().Order = await _order.GetOrderByOrderId(orderDetailVM.OrderItems.First().OrderID);
            return View(orderDetailVM);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _order.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
