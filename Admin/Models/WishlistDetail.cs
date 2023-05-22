using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class WishlistDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid WishlistId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public string Image { get; set; }
        public Products Products { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
