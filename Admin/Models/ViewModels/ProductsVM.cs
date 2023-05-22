namespace Admin.Models.ViewModels
{
    public class ProductsVM
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public string BaseImage { get; set; }
        public string AltImage { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public bool IsFeatured { get; set; }
        public List<string>? Images { get; set; } = new List<string>();
        //public List<string>? Types { get; set; } = new List<string>();
        public string Brand { get; set; }
        public Guid BrandsId { get; set; }
        public List<Products> FirstFourItems { get; set; }
        public List<Products> lastFourItems { get; set; }

    }

    public class UpdateImages
    {
        public Guid Id { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
