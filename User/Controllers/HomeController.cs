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
        private ITypes _Types;
        private IImages _Images;
        private readonly IConfiguration _configuration;
        public HomeController(IWebHostEnvironment environment,IBrands brands, IConfiguration configuration , ITypes types, IProducts products , IImages images)
        {
            _environment = environment;
            _brands = brands;
            _configuration = configuration;
            _products = products;
            _Types = types;
            _Images = images;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Brands = await _brands.GetBrands();
            homeVM.Products = await _products.GetFeaturedProduct();

            foreach (var product in homeVM.Products)
            {
                //var BaseimagePath = Path.Combine(_environment.WebRootPath, "images", product.BaseImage);
                //if (System.IO.File.Exists(BaseimagePath))
                //{
                //    var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(BaseimagePath));
                //    product.BaseImage = $"data:image/jpeg;base64,{imageData}";
                //}
                //var altimagePath = Path.Combine(_environment.WebRootPath, "images", product.AltImage);
                //if (System.IO.File.Exists(altimagePath))
                //{
                //    var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(altimagePath));
                //    product.AltImage = $"data:image/jpeg;base64,{imageData}";
                //}

                //product.Types = await _Types.GetTypeByProductId(product.Id);
            }

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

        [HttpGet]
        public async Task<ReturnResponse> GetProductById(Guid Id)
        {
            var response = new ReturnResponse();
            ProductsVM productsVM = new ProductsVM();
            Products products = await _products.GetProductById(Id);
            //products.Types = await _Types.GetTypeByProductId(Id);
            products.Images = await _Images.GetImageByProductId(Id);
            Brands brands = await _brands.GetBrandById(products.BrandsId);
            if (products != null)
            {
                var BaseimagePath = Path.Combine(_environment.WebRootPath, "images", products.BaseImage);
                if (System.IO.File.Exists(BaseimagePath))
                {
                    var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(BaseimagePath));
                    products.BaseImage = $"data:image/jpeg;base64,{imageData}";
                }
                var altimagePath = Path.Combine(_environment.WebRootPath, "images", products.AltImage);
                if (System.IO.File.Exists(altimagePath))
                {
                    var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(altimagePath));
                    products.AltImage = $"data:image/jpeg;base64,{imageData}";
                }
                //products.Types = (List<Types>)types.ToList().Where(x => x.ProductsId == products.Id);
                //products.Images = (List<Images>)Images.ToList().Where(x => x.ProductsId == products.Id && x.ImageName != products.BaseImage && x.ImageName != products.AltImage);
            }
            productsVM.NameEn = products.NameEn;
            productsVM.Price = products.Price;
            productsVM.Size = products.Size;
            productsVM.DescriptionEn = products.DescriptionEn;
            productsVM.Brand = brands.NameEn;
            productsVM.BaseImage = products.BaseImage;
            productsVM.AltImage = products.AltImage;
            productsVM.IsFeatured = products.IsFeatured;
            //foreach (var item in products.Types)
            //{
            //    productsVM.Types.Add(item.Value);
            //}
            foreach (var item in products.Images)
            {
                var image = Path.Combine(_environment.WebRootPath, "images", item.ImageName);
                if (System.IO.File.Exists(image))
                {
                    var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(image));
                    item.ImageName = $"data:image/jpeg;base64,{imageData}";
                }
                productsVM.Images.Add(item.ImageName);
            }
            response.Data = productsVM;
            return response;
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
