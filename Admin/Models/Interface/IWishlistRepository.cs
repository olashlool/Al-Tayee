namespace Admin.Models.Interface
{
    public interface IWishlistRepository
    {
        Task<int> AddItem(Guid productId, string img);
        Task<int> AddItem(Guid productId, int qty, string img);
        Task<int> RemoveItem(Guid productId, string img);
        Task<Wishlist> GetUserWishlist();
        Task<IEnumerable<WishlistDetail>> GetWishlistProductByUserId(string userId);
        Task RemoveWishlistProducts(IEnumerable<WishlistDetail> wishlistProducts);
        Task<Wishlist> GetWishlist(string userId);
        Task<int> GetWishlistItemCount();

        //Task<int> AddItem(Guid bookId);
        //Task<int> AddItemWithQty(Guid bookId, int qty, string image);
        //Task<int> RemoveItem(Guid bookId);
        //Task<Wishlist> GetUserWishlist();
        //Task<int> GetWishlistItemCount();
        //Task<Wishlist> GetWishlist(string userId);
    }
}
