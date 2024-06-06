using Admin.Data;
using Admin.Models;
using Admin.Models.ViewModels;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartRepository _shop;
        private readonly IOrder _order;
        private readonly IProducts _product;
        private readonly IAddresses _addresses;

        public CheckoutController(UserManager<ApplicationUser> userManager, ICartRepository shop, IOrder order, IProducts product, IAddresses addresses)
        {
            _userManager = userManager;
            _shop = shop;
            _order = order;
            _product = product;
            _addresses = addresses;
        }

        public async Task<IActionResult> Completed(Guid order)
        {
            var orderById = await _order.GetOrderByOrderId(order);
            return View(orderById);
        }

        public async Task<IActionResult> Index()
        {
            var checkoutVM = new Admin.Models.ViewModels.CheckoutVM();
            checkoutVM.Addresses = await _addresses.GetAddressesByUserId();
            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Admin.Models.ViewModels.CheckoutVM checkoutInput)
        {
            if (checkoutInput.Order.FullLocation != "0")
            {
                var getFullAddress = _addresses.GetAddressById(Convert.ToInt32(checkoutInput.Order.FullLocation));
                Order order = new Order
                {
                    UserID = getFullAddress.Result.UserId,
                    FirstName = getFullAddress.Result.FirstName,
                    LastName = getFullAddress.Result.LastName,
                    Address = getFullAddress.Result.Address1,
                    Address2 = getFullAddress.Result.Address2,
                    State = getFullAddress.Result.City,
                    City = getFullAddress.Result.City,
                    Country = getFullAddress.Result.Country,
                    Email = getFullAddress.Result.Email,
                    Fax = getFullAddress.Result.FaxNumber,
                    Phone = getFullAddress.Result.PhoneNumber,
                    Zip = checkoutInput.Order.Zip,
                    Timestamp = checkoutInput.Order.Timestamp+ ";",
                    OrderStatus = checkoutInput.Order.OrderStatus,
                    PaymentMethod = checkoutInput.Order.PaymentMethod,
                    PaymentStstus = checkoutInput.Order.PaymentStstus
                };

                var createdOrder = await _order.CreateOrder(order);
                var latestOrder = await _order.GetLatestOrderForUser(checkoutInput.Order.UserID);

                IEnumerable<CartDetail> cartItems = await _shop.GetCartProductByUserId(checkoutInput.Order.UserID);
                IList<OrderItems> orderItems = new List<OrderItems>();
                double total = 0;

                foreach (var cartItem in cartItems)
                {
                    OrderItems orderItem = new OrderItems
                    {
                        OrderID = latestOrder.ID,
                        ProductID = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        ImageProduct = cartItem.Image                        
                    };
                    orderItems.Add(orderItem);
                    var product = await _product.GetProductById(cartItem.ProductId);
                    total += product.Price * cartItem.Quantity;
                }
                await _order.CreateOrderItems(orderItems);
                await _shop.RemoveCartProducts(cartItems);

                return RedirectToAction("Completed", "Checkout", new { order = createdOrder.ID });
            }
            else if(checkoutInput.Order.FirstName != null && checkoutInput.Order.LastName != null && checkoutInput.Order.Address != null && checkoutInput.Order.City != null && checkoutInput.Order.Email != null && checkoutInput.Order.Phone != null)
            {
                Order order = new Order
                {
                    UserID = checkoutInput.Order.UserID,
                    FirstName = checkoutInput.Order.FirstName,
                    LastName = checkoutInput.Order.LastName,
                    Address = checkoutInput.Order.Address,
                    Address2 = checkoutInput.Order.Address2,
                    State = checkoutInput.Order.City,
                    City = checkoutInput.Order.City,
                    Country = checkoutInput.Order.Country,
                    Email = checkoutInput.Order.Email,
                    Fax = checkoutInput.Order.Fax,
                    Phone = checkoutInput.Order.Phone,
                    Zip = checkoutInput.Order.Zip,
                    Timestamp = checkoutInput.Order.Timestamp,
                    OrderStatus = checkoutInput.Order.OrderStatus,
                    PaymentMethod = checkoutInput.Order.PaymentMethod,
                    PaymentStstus = checkoutInput.Order.PaymentStstus
                };

                var createdOrder = await _order.CreateOrder(order);
                var latestOrder = await _order.GetLatestOrderForUser(checkoutInput.Order.UserID);

                IEnumerable<CartDetail> cartItems = await _shop.GetCartProductByUserId(checkoutInput.Order.UserID);
                IList<OrderItems> orderItems = new List<OrderItems>();
                double total = 0;

                foreach (var cartItem in cartItems)
                {
                    OrderItems orderItem = new OrderItems
                    {
                        OrderID = latestOrder.ID,
                        ProductID = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
						ImageProduct = cartItem.Image
					};
                    orderItems.Add(orderItem);
                    var product = await _product.GetProductById(cartItem.ProductId);
                    total += product.Price * cartItem.Quantity;
                }

                double finalCost = total * 1.1;

                await _order.CreateOrderItems(orderItems);
                await _shop.RemoveCartProducts(cartItems);

                return RedirectToAction("Completed", "Checkout", new { order = createdOrder.ID });
            }
            return View();
        }
    }
}
