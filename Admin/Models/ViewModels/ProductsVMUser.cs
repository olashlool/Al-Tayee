namespace Admin.Models.ViewModels
{
    public class ProductsVMUser
    {
        public Products Products { get; set; }
        public List<Products> FirstFourItems { get; set; }
        public List<Products> lastFourItems { get; set; }
    }
}
