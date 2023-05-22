using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<WishlistDetail> WishlistsDetail { get; set; }
    }
}
