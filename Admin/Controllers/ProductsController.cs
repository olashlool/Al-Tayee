using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.VisualBasic;
using System.Linq;

namespace Admin.Controllers
{
    public class ProductsController : Controller
    {
        private IWebHostEnvironment _environment;
        private IProducts _Products;
        private ITypes _Types;
        private IImages _Images;
        private IBrands _Brands;
        string culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

        public ProductsController(IWebHostEnvironment webHostEnvironment, IProducts products, ITypes types, IImages images, IBrands brands)
        {
            _environment = webHostEnvironment;
            _Products = products;
            _Types = types;
            _Images = images;
            _Brands = brands;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _Products.GetProducts();
            ViewBag.Brands = await _Brands.GetBrands();
            return View(products);
        }
        public async Task<IActionResult> Details(Guid Id)
        {
            var response = new ReturnResponse();
            Products products = await _Products.GetProductById(Id);
            ProductsVM productsVM = new ProductsVM();
            productsVM.NameEn = products.NameEn;
            productsVM.NameAr = products.NameAr;
            productsVM.DescriptionEn = products.DescriptionEn;
            productsVM.DescriptionAr = products.DescriptionAr;
            productsVM.IsFeatured = products.IsFeatured;
            productsVM.Price = products.Price;
            productsVM.Size = products.Size;

            var imageData = string.Empty;
            Brands brands = await _Brands.GetBrandById(products.BrandsId);
            var nameBrand = culture.StartsWith("en") ? brands.NameEn : brands.NameAr;
            productsVM.Brand = nameBrand;
            List<Images> Images = await _Images.GetImageByProductId(Id);
            foreach (var item in Images)
            {
                productsVM.Images.Add(item.ImageName);
            }
            return View(productsVM);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _Products.GetProductById(id);
            product.Images = await _Images.GetImageByProductId(id);
            product.Brands = await _Brands.GetBrandById(product.BrandsId);
            ViewBag.Brands = await _Brands.GetBrands();
            return View(product);
        }
        [HttpPost]
        public async Task<List<string>> AddImages(List<IFormFile> Images, List<string> NameOfImages)
        {
            List<string> FilesName = new List<string>();
            var nameOfImage = "";
            for (int i = 0; i < NameOfImages.Count; i++)
            {
                var fileExtension = Path.GetExtension(Images[i].FileName);
                nameOfImage = $"{NameOfImages[i]}{fileExtension}";
                var filePath = Path.Combine(_environment.WebRootPath, "images", nameOfImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Images[i].CopyToAsync(stream);
                }
                FilesName.Add(nameOfImage);
            }
            return FilesName;
        }
        [HttpPost]
        public async Task<ReturnResponse> AddProduct([FromBody] ProductsVM productVM)
        {
            Products products = new Products();
            ReturnResponse response = new ReturnResponse();
            products = new Products
            {
                Id = Guid.NewGuid(),
                NameEn = productVM.NameEn,
                NameAr = productVM.NameAr,
                DescriptionEn = productVM.DescriptionEn,
                DescriptionAr = productVM.DescriptionAr,    
                Price = productVM.Price,
                Size = productVM.Size,
                IsFeatured = productVM.IsFeatured,
                BaseImage = productVM.BaseImage,
                AltImage = productVM.AltImage,
                BrandsId = productVM.BrandsId,
            };

            if (await _Products.CreateProduct(products) > 0)
            {
                var count = 0;
                foreach (var item in productVM.Images)
                {
                    count++;
                    var Image = new Images
                    {
                        ProductsId = products.Id,
                        ImageName = item
                    };
                    //if (count > 0)
                        await _Images.CreateImages(Image);
                }

                response.IsSuccess = true;
                response.Status = Status.Success;
                response.Message = "Done";
                response.Data = products;
            }
            else
            {
                response.IsSuccess = false;
                response.Status = Status.Error;
                response.Message = "Oops... Somthing went wrong \nplease check your data and try again";
            }

            return response;
        }

        [HttpPost]
        public async Task<ReturnResponse> EditProduct([FromBody] ProductsVM productVM)
        {
            ReturnResponse response = new ReturnResponse();
            List<Images> listOfImages = new List<Images>();

            foreach (var item in productVM.Images)
            {
                var images = new Images
                {
                    ImageName = item,
                    ProductsId = productVM.Id,
                };
                await _Images.CreateImages(images);
                listOfImages.Add(images);
            }
            Products products = new Products
            {
                Id = productVM.Id,
                NameEn = productVM.NameEn,
                NameAr = productVM.NameAr,
                DescriptionEn = productVM.DescriptionEn,
                DescriptionAr = productVM.DescriptionAr,
                Price = productVM.Price,
                Size = productVM.Size,
                IsFeatured = productVM.IsFeatured,
                BaseImage = productVM.BaseImage,
                AltImage = productVM.AltImage,
                BrandsId = productVM.BrandsId,
                Images= listOfImages
            };
            if (await _Products.UpdateProduct(productVM.Id, products) > 0)
            {
                response.IsSuccess = true;
                response.Status = Status.Success;
                response.Message = "Done";
            }
            else
            {
                response.IsSuccess = false;
                response.Status = Status.Error;
                response.Message = "Oops... Somthing went wrong \nplease check your data and try again";
            }

            return response;
        }
        [HttpPost]
        public async Task<List<string>> EditImages(List<IFormFile> Images, List<string> newNameOfImages, List<string> oldNameOfImages, string id)
        {
            Guid guid = Guid.Parse(id);
            var ImagesByProductId = await _Images.GetImageByProductId(guid);

            foreach (var item in ImagesByProductId)
            {
                await _Images.DeleteImages(item.Id);
            }

            List<string> FilesName = new List<string>();
            var nameOfImage = "";
            for (int i = 0; i < newNameOfImages.Count; i++)
            {
                var fileExtension = Path.GetExtension(Images[i].FileName);
                nameOfImage = $"{newNameOfImages[i]}{fileExtension}";
                var filePath = Path.Combine(_environment.WebRootPath, "images", nameOfImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Images[i].CopyToAsync(stream);
                }
                FilesName.Add(nameOfImage);
            }

            foreach (var item in oldNameOfImages)
            {
                FilesName.Add(item);
            }
            return FilesName;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            await _Products.DeleteProduct(Id);
            return Ok();
        }
    }
}
