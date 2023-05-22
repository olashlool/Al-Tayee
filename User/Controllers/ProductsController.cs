using Microsoft.AspNetCore.Mvc;
using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;

namespace User.Controllers
{
    public class ProductsController : Controller
    {

        private IWebHostEnvironment _environment;
        private readonly IBrands _brands;
        private readonly IProducts _products;
        private ITypes _Types;
        private IImages _Images;
        private readonly IConfiguration _configuration;
        public ProductsController(IWebHostEnvironment environment, IBrands brands, IConfiguration configuration, ITypes types, IProducts products, IImages images)
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
            var listProducts = await _products.GetProducts();

            foreach (var product in listProducts)
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

            return View(listProducts);
        }
        public async Task<IActionResult> FeaturedProduct(string sortOrder = "")
        {
            var listOfFeaturedProduct = await _products.GetFeaturedProduct();
            //foreach (var item in listOfFeaturedProduct)
            //{
            //    List<Types> types = await _Types.GetTypeByProductId(item.Id);
            //    item.Types = types;
            //}
            // Apply the sorting based on the sortOrder parameter
            switch (sortOrder)
            {
                case "lowToHigh":
                    listOfFeaturedProduct = listOfFeaturedProduct.OrderBy(p => p.Price).ToList();
                    break;
                case "highToLow":
                    listOfFeaturedProduct = listOfFeaturedProduct.OrderByDescending(p => p.Price).ToList();
                    break;
                case "aToZ":
                    listOfFeaturedProduct = listOfFeaturedProduct.OrderBy(p => p.NameEn).ToList();
                    break;
                case "zToA":
                    listOfFeaturedProduct = listOfFeaturedProduct.OrderByDescending(p => p.NameEn).ToList();
                    break;
                default:
                    // Handle default case or no sorting
                    break;
            }
            return View(listOfFeaturedProduct);
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            //var response = new ReturnResponse();
            ProductsVMUser productsVM = new ProductsVMUser();
            productsVM.Products = await _products.GetProductById(Id);

            //Get Brand for this Product
            var brand = await _brands.GetBrandById(productsVM.Products.BrandsId);
            productsVM.Products.Brands = brand;

            //Get Type for this Product
            //List<Types> types = await _Types.GetTypeByProductId(productsVM.Products.Id);
            //productsVM.Products.Types = types;

            // Get image for this product
            List<Images> images = await _Images.GetImageByProductId(productsVM.Products.Id);
            productsVM.Products.Images = images;

            List<Products> productList = await _products.GetProductByBrand(productsVM.Products.BrandsId); // Replace with your list of products
            if(productList.Count >= 4)
            {
                productsVM.FirstFourItems = productList.Take(4).ToList();
                productsVM.lastFourItems = productList.TakeLast(4).ToList();
            }
            else
            {
                productsVM.FirstFourItems = productList;
                productsVM.lastFourItems = productList;
            }

            //foreach (var typeFirstFourItems in productsVM.FirstFourItems)
            //{
            //    var type = await _Types.GetTypeByProductId(productsVM.Products.Id);
            //    typeFirstFourItems.Types = type;
            //}
            //foreach (var typelastFourItems in productsVM.lastFourItems)
            //{
            //    var type = await _Types.GetTypeByProductId(productsVM.Products.Id);
            //    typelastFourItems.Types = type;
            //}
            //products.Types = await _Types.GetTypeByProductId(Id);
            //products.Images = await _Images.GetImageByProductId(Id);
            //Brands brands = await _brands.GetBrandById(products.BrandsId);
            //if (products != null)
            //{
            //    var BaseimagePath = Path.Combine(_environment.WebRootPath, "images", products.BaseImage);
            //    if (System.IO.File.Exists(BaseimagePath))
            //    {
            //        var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(BaseimagePath));
            //        products.BaseImage = $"data:image/jpeg;base64,{imageData}";
            //    }
            //    var altimagePath = Path.Combine(_environment.WebRootPath, "images", products.AltImage);
            //    if (System.IO.File.Exists(altimagePath))
            //    {
            //        var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(altimagePath));
            //        products.AltImage = $"data:image/jpeg;base64,{imageData}";
            //    }
            //    //products.Types = (List<Types>)types.ToList().Where(x => x.ProductsId == products.Id);
            //    //products.Images = (List<Images>)Images.ToList().Where(x => x.ProductsId == products.Id && x.ImageName != products.BaseImage && x.ImageName != products.AltImage);
            //}
            //productsVM.NameAr = products.NameAr;
            //productsVM.NameEn = products.NameEn;
            //productsVM.Price = products.Price;
            //productsVM.Size = products.Size;
            //productsVM.DescriptionAr = products.DescriptionAr;
            //productsVM.DescriptionEn = products.DescriptionEn;
            //productsVM.Brand = brands.NameEn;
            //productsVM.BaseImage = products.BaseImage;
            //productsVM.AltImage = products.AltImage;
            //productsVM.IsFeatured = products.IsFeatured;
            //foreach (var item in products.Types)
            //{
            //    productsVM.Types.Add(item.Value);
            //}
            //foreach (var item in products.Images)
            //{
            //    var image = Path.Combine(_environment.WebRootPath, "images", item.ImageName);
            //    if (System.IO.File.Exists(image))
            //    {
            //        var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(image));
            //        item.ImageName = $"data:image/jpeg;base64,{imageData}";
            //    }
            //    productsVM.Images.Add(item.ImageName);
            //}
            //response.Data = productsVM;            
            return View(productsVM);
        }
    }
}
