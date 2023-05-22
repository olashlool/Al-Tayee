namespace Admin.Models
{
    public class Types
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid ProductsId { get; set; }
        public Products Products { get; set; }
    }

    public class TypesVM
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public List<string> Types { get; set; }
    }
}
