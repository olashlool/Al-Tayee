using Microsoft.AspNetCore.Mvc;
using Admin.Models.Interface;
using Admin.Models;

namespace User.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProducts _products;
        private ITypes _Types;

        public SearchController(IProducts products, ITypes types)
        {
            _products = products;
            _Types = types;
        }
        public async Task<IActionResult> Index(string searchTerm = "", string sortOrder = "")
        {
            var listOfProduct = await _products.GetProducts();

            // Apply the search based on the searchTerm parameter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                listOfProduct = listOfProduct.Where(p => p.NameEn.Contains(searchTerm)).ToList();
            }
            //foreach (var item in listOfProduct)
            //{
            //    List<Types> types = await _Types.GetTypeByProductId(item.Id);
            //    item.Types = types;
            //}
            // Pass the filtered products and searchTerm to the view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Count = listOfProduct.Count;
            // Apply the sorting based on the sortOrder parameter
            switch (sortOrder)
            {
                case "lowToHigh":
                    listOfProduct = listOfProduct.OrderBy(p => p.Price).ToList();
                    break;
                case "highToLow":
                    listOfProduct = listOfProduct.OrderByDescending(p => p.Price).ToList();
                    break;
                case "aToZ":
                    listOfProduct = listOfProduct.OrderBy(p => p.NameEn).ToList();
                    break;
                case "zToA":
                    listOfProduct = listOfProduct.OrderByDescending(p => p.NameEn).ToList();
                    break;
                default:
                    // Handle default case or no sorting
                    break;
            }
            return View(listOfProduct);
        }
    }
}
