namespace Admin.Models
{
    public class Products
    {
        public Guid Id { get; set; }

        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string BaseImage { get; set; }
        public string AltImage { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public bool IsFeatured { get; set; }
        public List<Images> Images { get; set; }
        public Guid BrandsId { get; set; }
        public Brands Brands { get; set; }
    }
}
