namespace Admin.Models.ViewModels
{
    public class HomeVM
    {
        public List<Brands> Brands { get; set; }
        public List<Products> Products { get; set; }
        public List<GridImage> GridImages { get; set; }
        public List<BackgroundSection> BackgroundSection { get; set; }
    }
}
