namespace Admin.Models.Interface
{
    public interface ICartRepository
    {
        Task<int> AddItem(Guid bookId);
        Task<int> AddItem(Guid bookId, int qty, string image);
        Task<int> RemoveItem(Guid bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        public Task<IEnumerable<CartDetail>> GetCartProductByUserId(string userId);
        public Task RemoveCartProducts(IEnumerable<CartDetail> cartProduct);
    }
}
