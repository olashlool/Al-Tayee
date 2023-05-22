using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ShoppingCartId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public string Image { get; set; }
        public Products Products { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
