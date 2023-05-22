using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace User.Controllers
{
    public class BrandsController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IBrands _brands;
        private readonly IProducts _products;
        private readonly IConfiguration _configuration;
        private ITypes _Types;

        public BrandsController(IWebHostEnvironment environment, IBrands brands, IConfiguration configuration, IProducts products, ITypes types)
        {
            _environment = environment;
            _brands = brands;
            _configuration = configuration;
            _products = products;
            _Types = types;
        }
        [HttpGet]
        [Route("Brands/Index")]
        public async Task<IActionResult> Index()
        {
            var listOfBrands = await _brands.GetBrands();
            return View(listOfBrands);
        }
        public async Task<IActionResult> Products(Guid brandId, string sortOrder = "")
        {
            var listOfProducts = await _products.GetProductByBrand(brandId);
            if(listOfProducts.Count > 0) {
                listOfProducts.First().Brands = await _brands.GetBrandById(brandId);
                //foreach (var item in listOfProducts)
                //{
                //    List<Types> types = await _Types.GetTypeByProductId(item.Id);
                //    item.Types = types;
                //}
                // Apply the sorting based on the sortOrder parameter
                switch (sortOrder)
                {
                    case "lowToHigh":
                        listOfProducts = listOfProducts.OrderBy(p => p.Price).ToList();
                        break;
                    case "highToLow":
                        listOfProducts = listOfProducts.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "aToZ":
                        listOfProducts = listOfProducts.OrderBy(p => p.NameEn).ToList();
                        break;
                    case "zToA":
                        listOfProducts = listOfProducts.OrderByDescending(p => p.NameEn).ToList();
                        break;
                    default:
                        // Handle default case or no sorting
                        break;
                }
                return View(listOfProducts);
            }
            return View(null);
        }
        //[HttpGet]
        //public async Task<IList<Brands>> GetAllBrands()
        //{
        //    return await _brands.GetBrands();
        //}
    }
}
