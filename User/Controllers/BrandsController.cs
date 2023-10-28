using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IBrands _brands;
        private readonly IProducts _products;
        private readonly IConfiguration _configuration;
        private readonly ITypes _types;
        private readonly IMemoryCache _cache;

        public BrandsController(IWebHostEnvironment environment, IBrands brands, IConfiguration configuration, IProducts products, ITypes types, IMemoryCache cache)
        {
            _environment = environment;
            _brands = brands;
            _configuration = configuration;
            _products = products;
            _types = types;
            _cache = cache;
        }

        [HttpGet]
        [Route("Brands/Index")]
        public async Task<IActionResult> Index()
        {
            var listOfBrands = await _brands.GetBrands();
            return View(listOfBrands);
        }

        public async Task<IActionResult> Products(Guid brandId, string sortOrder)
        {
            var listOfProducts = await _cache.GetOrCreateAsync($"Products_{brandId}", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(5);
                return await _products.GetProductByBrand(brandId);
            });

            if (listOfProducts.Count >= 0)
            {
                var brand = await _brands.GetBrandById(brandId);
                listOfProducts.ForEach(p => p.Brands = brand);

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
            }
            return View(listOfProducts);
        }
    }
}
