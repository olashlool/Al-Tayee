using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IProducts _products;
        private IOrder _order;
        private IBrands _brand;
        private IContactUs _contactUs;

        public DashboardController(IProducts products, IOrder order, IBrands brands, IContactUs contactUs)
        {
            _products = products;
            _order = order;
            _brand = brands;
            _contactUs = contactUs;
        }
        public async Task<IActionResult> Index()
        {
            var homeAdminVM = new HomeAdminVM();

            var orders = await _order.GetOrders();
            var products = await _products.GetProducts();

            // Define the expected date format
            string[] formats = { "M/d/yyyy h:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt" }; // Add more formats if needed
            var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;

            // Sort orders by timestamp
            orders = orders
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

            homeAdminVM.Orders = orders;
            homeAdminVM.Products = await _products.GetFeaturedProduct();
            homeAdminVM.ContactUs = await _contactUs.GetFeedback();

            var brands = await _brand.GetBrands();
            var ordersCompleted = orders.Where(x => x.OrderStatus == "Completed").ToList();
            var ordersPending = orders.Where(x => x.OrderStatus == "Pending").ToList();

            ViewBag.CountOfBrands = brands.Count();
            ViewBag.CountOfOrdersCompleted = ordersCompleted.Count();
            ViewBag.CountOfOrdersPending = ordersPending.Count();
            ViewBag.CountOfProduct = products.Count();

            return View(homeAdminVM);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _order.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult Change()
        {
            string? culture = Request.Query["culture"];
            Console.WriteLine("new selected language: " + culture);
            if (culture != null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }


            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
    }
}
