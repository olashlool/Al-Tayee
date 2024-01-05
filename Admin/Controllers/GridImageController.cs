using Admin.Models;
using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Admin.Controllers
{
    public class GridImageController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IGridImage _gridImage;
        private readonly IConfiguration _configuration;
        public GridImageController(IWebHostEnvironment environment, IGridImage gridImage, IConfiguration configuration)
        {
            _environment = environment;
            _gridImage = gridImage;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var listOfgridImages = await _gridImage.GetGridImages();
            return View(listOfgridImages);
        }
        [HttpPost]
        public async Task<ReturnResponse> AddGridImage(List<IFormFile> Images, List<string> NameOfImages, string OldImages)
        {
            List<GridImage> oldImagesList = JsonConvert.DeserializeObject<List<GridImage>>(OldImages);
            List<int> missingIds = FindMissingIds(oldImagesList);
            foreach (var item in missingIds)
            {
                await _gridImage.DeleteGridImage(item);
            }
            List<string> FilesName = new List<string>();

            var nameOfImage = "";
            for (int i = 0; i < NameOfImages.Count; i++)
            {
                GridImage newGridImage = new GridImage();

                var fileExtension = Path.GetExtension(Images[i].FileName);
                nameOfImage = $"{NameOfImages[i]}{fileExtension}";
                var filePath = Path.Combine(_environment.WebRootPath, "images/Grid", nameOfImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Images[i].CopyToAsync(stream);
                }
                newGridImage.ImageUrl = "/images/grid/" + nameOfImage;
                newGridImage.Id = missingIds[i];
                await _gridImage.CreateGridImage(newGridImage);

                FilesName.Add(nameOfImage);

            }
            return null;
        }

        static List<int> FindMissingIds(List<GridImage> list)
        {
            List<int> allIds = list.Select(item => item.Id).ToList();
            List<int> missingIds = Enumerable.Range(1, 5).Except(allIds).ToList();

            return missingIds;
        }
    }
}
