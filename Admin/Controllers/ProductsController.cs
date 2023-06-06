using Admin.Models.Interface;
using Admin.Models.ViewModels;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class ProductsController : Controller
    {
        private IWebHostEnvironment _environment;
        private IProducts _Products;
        private ITypes _Types;
        private IImages _Images;
        private IBrands _Brands;
        public ProductsController(IWebHostEnvironment webHostEnvironment, IProducts products, ITypes types, IImages images, IBrands brands)
        {
            _environment = webHostEnvironment;
            _Products = products;
            _Types = types;
            _Images = images;
            _Brands = brands;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(Guid Id)
        {
            var response = new ReturnResponse();
            Products products = await _Products.GetProductById(Id);
            ProductsVM productsVM = new ProductsVM();
            productsVM.NameEn = products.NameEn;
            productsVM.DescriptionEn = products.DescriptionEn;
            productsVM.IsFeatured = products.IsFeatured;
            productsVM.Price = products.Price;
            productsVM.Size = products.Size;

            var imageData = string.Empty;
            var BaseimagePath = Path.Combine(_environment.WebRootPath, "images", products.BaseImage);
            if (System.IO.File.Exists(BaseimagePath))
            {
                string imageFormat = Path.GetExtension(BaseimagePath).TrimStart('.');
                imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(BaseimagePath));
                productsVM.BaseImage = $"data:image/{imageFormat};base64,{imageData}";
            }
            var altimagePath = Path.Combine(_environment.WebRootPath, "images", products.AltImage);
            if (System.IO.File.Exists(altimagePath))
            {
                string imageFormat = Path.GetExtension(altimagePath).TrimStart('.');
                imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(altimagePath));
                productsVM.AltImage = $"data:image/{imageFormat};base64,{imageData}";
            }

            Brands brands = await _Brands.GetBrandById(products.BrandsId);
            productsVM.Brand = brands.NameEn;
            List<Images> Images = await _Images.GetImageByProductId(Id);
            foreach (var item in Images)
            {
                var img = string.Empty;
                var fileName = Path.Combine(_environment.WebRootPath, "images", item.ImageName);
                if (System.IO.File.Exists(fileName))
                {
                    string imageFormat = Path.GetExtension(fileName).TrimStart('.');
                    imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
                    img = $"data:image/{imageFormat};base64,{imageData}";
                }
                productsVM.Images.Add(img);
            }
            //List<Types> types = await _Types.GetTypeByProductId(Id);
            //foreach (var type in types)
            //{
            //    productsVM.Types.Add(type.Value);
            //}
            return View(productsVM);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _Products.GetProductById(id);
            product.Images = await _Images.GetImageByProductId(id);
            ViewBag.Brands = await _Brands.GetBrands();
            return View(product);
        }

        [HttpPost]
        public async Task<List<string>> AddImages(List<IFormFile> Images)
        {
            var count = 0;
            //var fileName = string.Empty;
            List<string> FilesName = new List<string>();
            foreach (var image in Images)
            { // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                var fileExtension = Path.GetExtension(image.FileName);
                fileName = $"{fileName}_{DateTime.UtcNow.Ticks}{fileExtension}";

                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }


                //fileName = Path.GetFileName("Img_" + Guid.NewGuid() + image.FileName );
                //var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    var f = image.CopyToAsync(fileStream);
                //}
                count++;
                FilesName.Add(fileName);
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
                DescriptionEn = productVM.DescriptionEn,
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
                    if (count > 2)
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

        //[HttpPost]
        //public async Task<ReturnResponse> EditProduct([FromBody] ProductsVM productVM)
        //{
        //    ReturnResponse response = new ReturnResponse();
        //    Products products = new Products
        //    {
        //        Id = productVM.Id,
        //        NameAr = productVM.NameAr,
        //        NameEn = productVM.NameEn,
        //        DescriptionEn = productVM.DescriptionEn,
        //        DescriptionAr = productVM.DescriptionAr,
        //        Price = productVM.Price,
        //        Size = productVM.Size,
        //        IsFeatured = productVM.IsFeatured,
        //        BaseImage = productVM.BaseImage,
        //        AltImage = productVM.AltImage,
        //        //BrandId = productVM.BrandId,
        //    };
        //    foreach (var item in productVM.UpdateTypes)
        //    {
        //        var Type = new Types
        //        {
        //            ProductsId = item.Id,
        //            Value = item.Value
        //        };
        //        await _Types.UpdateType(item.Id, Type);
        //    }

        //    var fileName = string.Empty;
        //    foreach (var item in productVM.Images)
        //    {
        //        foreach (var ImageItem in item.Images)
        //        {
        //            fileName = Path.GetFileName("Img_" + ImageItem.FileName + Guid.NewGuid());
        //            var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await ImageItem.CopyToAsync(fileStream);
        //            }

        //            var newImage = new Images
        //            {
        //                Id = item.Id,
        //                ImageName = fileName,
        //                ProductId = productVM.Id
        //            };
        //            await _Images.UpdateImages(item.Id, newImage);
        //        }
        //    }
        //    if (await _Products.UpdateProduct(productVM.Id, products) > 0)
        //    {
        //        response.IsSuccess = true;
        //        response.Status = Status.Success;
        //        response.Message = "Done";
        //    }
        //    else
        //    {
        //        response.IsSuccess = false;
        //        response.Status = Status.Error;
        //        response.Message = "Oops... Somthing went wrong \nplease check your data and try again";
        //    }

        //    return response;
        //}

        [HttpGet]
        public async Task<ReturnResponse> GetAllProducts()
        {
            var response = new ReturnResponse();
            List<Products> products = new List<Products>();
            List<Products> Allproducts = new List<Products>();

            products = await _Products.GetProducts();
            //List<Types> types = await _Types.GetTypes();

            if (products != null)
            {
                foreach (var product in products)
                {
                    var BaseimagePath = Path.Combine(_environment.WebRootPath, "images", product.BaseImage);
                    if (System.IO.File.Exists(BaseimagePath))
                    {
                        var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(BaseimagePath));
                        product.BaseImage = $"data:image/jpeg;base64,{imageData}";
                    }
                    var altimagePath = Path.Combine(_environment.WebRootPath, "images", product.AltImage);
                    if (System.IO.File.Exists(altimagePath))
                    {
                        var imageData = Convert.ToBase64String(System.IO.File.ReadAllBytes(altimagePath));
                        product.AltImage = $"data:image/jpeg;base64,{imageData}";
                    }
                    //product.Types = await _Types.GetTypeByProductId(product.Id);
                    //product.Types = (List<Types>)types.ToList().Where(x => x.ProductsId == product.Id);
                }
            }
            response.Data = products;
            return response;
        }

        [HttpGet]
        public async Task<ReturnResponse> GetProductById(Guid Id)
        {
            var response = new ReturnResponse();
            Products products = await _Products.GetProductById(Id);
            List<Types> types = await _Types.GetTypes();
            List<Images> Images = await _Images.GetImages();

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
                products.Images = (List<Images>)Images.ToList().Where(x => x.ProductsId == products.Id && x.ImageName != products.BaseImage && x.ImageName != products.AltImage);
            }
            return response;
        }

        public string EncodeBase(string imgPath)
        {
            return imgPath;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            await _Products.DeleteProduct(Id);
            return Ok();
        }
    }
}
