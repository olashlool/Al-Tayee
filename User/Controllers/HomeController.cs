using Microsoft.AspNetCore.Mvc;
using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;

namespace User.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IBrands _brands;
        private readonly IProducts _products;
        private readonly IGridImage _gridImage;
        private ITypes _Types;
        private IImages _Images;
        private readonly IConfiguration _configuration;
        public HomeController(IWebHostEnvironment environment,IBrands brands, IConfiguration configuration , ITypes types, IProducts products , IImages images, IGridImage gridImage)
        {
            _environment = environment;
            _brands = brands;
            _configuration = configuration;
            _products = products;
            _Types = types;
            _Images = images;
            _gridImage = gridImage;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Brands = await _brands.GetBrands();
            homeVM.Products = await _products.GetFeaturedProduct();
            homeVM.GridImages = await _gridImage.GetGridImages();
            return View(homeVM);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult DeliveryAndReturns()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult ConditionsofUse()
        {
            return View();
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
