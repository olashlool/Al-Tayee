using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.Controllers
{
    public class BrandsController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IBrands _brands;
        private readonly IConfiguration _configuration;
        public BrandsController(IWebHostEnvironment environment, IBrands brands, IConfiguration configuration)
        {
            _environment = environment;
            _brands = brands;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var listOfBrands = await _brands.GetBrands();
            return View(listOfBrands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IList<Brands>> GetAllBrands()
        {
            return await _brands.GetBrands();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandsVM brandsVM)
        {
            //Handle the uploaded logo image
            if (brandsVM.ImageUpload != null && brandsVM.ImageUpload.First().Length > 0)
            {
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(brandsVM.ImageUpload.First().FileName);
                var fileExtension = Path.GetExtension(brandsVM.ImageUpload.First().FileName);
                fileName = $"{fileName}_{DateTime.UtcNow.Ticks}{fileExtension}";

                // Save the image file to the server
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await brandsVM.ImageUpload.First().CopyToAsync(stream);
                }

                // Set the brand's LogoUrl property
                brandsVM.Brands.ImageUrl = $"/images/{fileName}";

                await _brands.CreateBrand(brandsVM.Brands);
                return RedirectToAction("Index");
            }
            return View(brandsVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var brandsVM = new BrandsVM();
            brandsVM.Brands = await _brands.GetBrandById(id);
            return View(brandsVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile images, Brands brands)
        {
            //Handle the uploaded logo image
            if (images != null && images.Length > 0)
            {
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(images.FileName);
                var fileExtension = Path.GetExtension(images.FileName);
                fileName = $"{fileName}_{DateTime.UtcNow.Ticks}{fileExtension}";

                // Save the image file to the server
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await images.CopyToAsync(stream);
                }

                // Set the brand's LogoUrl property
                brands.ImageUrl = $"/images/{fileName}";
                await _brands.UpdateBrand(brands.Id, brands);
                return RedirectToAction("Index");

            }
            else if (brands.NameEn != null && brands.NameAr != null && (images == null))
            {
                await _brands.UpdateBrand(brands.Id, brands);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _brands.DeleteBrand(id);
            return RedirectToAction("Index");
        }
    }
}
