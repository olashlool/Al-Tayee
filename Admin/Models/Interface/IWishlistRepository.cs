namespace Admin.Models.Interface
{
    public interface IWishlistRepository
    {
        Task<int> AddItem(Guid bookId);
        Task<int> AddItemWithQty(Guid bookId, int qty, string image);
        Task<int> RemoveItem(Guid bookId);
        Task<Wishlist> GetUserWishlist();
        Task<int> GetWishlistItemCount(string userId = "");
        Task<Wishlist> GetWishlist(string userId);
    }
}
