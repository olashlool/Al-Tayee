using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.Controllers
{
    public class BackgroundSectionController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IBackgroundSection _backgroundSection;
        private readonly IConfiguration _configuration;
        public BackgroundSectionController(IWebHostEnvironment environment, IBackgroundSection backgroundSection, IConfiguration configuration)
        {
            _environment = environment;
            _backgroundSection = backgroundSection;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var listOfBackgroundSection = await _backgroundSection.GetBackgroundSection();
            return View(listOfBackgroundSection);
        }
        [HttpGet]
        public async Task<BackgroundSection> GetBackgroundSectionById(int id)
        {
            return await _backgroundSection.GetBackgroundSectionById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string NameOfImages, IFormFile images, string backgroundSection)
        {
            BackgroundSection backgroundSectionList = JsonConvert.DeserializeObject<BackgroundSection>(backgroundSection);

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
                backgroundSectionList.ImageUrl = $"/images/{fileName}";
                await _backgroundSection.UpdateBackgroundSection(backgroundSectionList.Id, backgroundSectionList);
                return RedirectToAction("Index");

            }
            else if (backgroundSectionList.TitleEn != null && backgroundSectionList.TitleAr != null && (images == null))
            {
                await _backgroundSection.UpdateBackgroundSection(backgroundSectionList.Id, backgroundSectionList);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
