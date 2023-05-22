namespace Admin.Models
{
    public class Images
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public Guid ProductsId { get; set; }
        public Products Products { get; set; }
    }
}
