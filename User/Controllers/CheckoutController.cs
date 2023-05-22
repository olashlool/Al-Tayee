using Admin.Data;
using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartRepository _shop;
        private readonly IOrder _order;
        private readonly IProducts _product;

        public CheckoutController(UserManager<ApplicationUser> userManager, ICartRepository shop, IOrder order, IProducts product)
        {
            _userManager = userManager;
            _shop = shop;
            _order = order;
            _product = product;
        }

        // Bind the Input object that contains all the required information for checkout to the property        [BindProperty]
        public async Task<IActionResult> Completed(Guid order)
        {
            var orderById = await _order.GetOrderByOrderId(order);
            return View(orderById);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Order checkoutInput)
        {
            if (ModelState.IsValid)
            {
                //ApplicationUser user = await _userManager.GetUserAsync(User);

                Order order = new Order
                {
                    UserID = checkoutInput.UserID,
                    FirstName = checkoutInput.FirstName,
                    LastName = checkoutInput.LastName,
                    Address = checkoutInput.Address,
                    Address2 = checkoutInput.Address2,
                    City = checkoutInput.City,
                    State = checkoutInput.State,
                    Country= checkoutInput.Country,
                    Email= checkoutInput.Email,
                    Fax= checkoutInput.Fax,
                    Phone= checkoutInput.Phone,
                    Zip = checkoutInput.Zip,
                    Timestamp = checkoutInput.Timestamp,
                    OrderStatus= checkoutInput.OrderStatus,
                    PaymentMethod= checkoutInput.PaymentMethod,
                    PaymentStstus= checkoutInput.PaymentStstus,
                };

                var idOrder = await _order.CreateOrder(order);

                order = await _order.GetLatestOrderForUser(checkoutInput.UserID);

                IEnumerable<CartDetail> cartItems = await _shop.GetCartProductByUserId(checkoutInput.UserID);
                IList<OrderItems> orderItems = new List<OrderItems>();
                double total = 0;

                foreach (var cartItem in cartItems)
                {
                    OrderItems orderItem = new OrderItems
                    {
                        OrderID = order.ID,
                        ProductID = cartItem.ProductId,
                        Quantity = cartItem.Quantity
                    };
                    orderItems.Add(orderItem);
                    var product = await _product.GetProductById(cartItem.ProductId);
                    total += product.Price * cartItem.Quantity;
                }

                double finalCost = total * 1.1;

                foreach (var item in orderItems)
                {
                    await _order.CreateOrderItem(item);
                }
                await _shop.RemoveCartProducts(cartItems);

                return RedirectToAction("Completed", "Checkout", new {order = idOrder.ID});
            }
            return View();
        }
    }
}
