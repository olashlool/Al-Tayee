namespace Admin.Models.Interface
{
    public interface ICartRepository
    {
        public Task<int> AddItem(Guid productId, string img);
        public Task<int> AddItem(Guid productId, int qty, string img);
        public Task<int> RemoveItem(Guid bookId, string img);
        public Task<ShoppingCart> GetUserCart();
        public Task<ShoppingCart> GetCart(string userId);
        public Task<int> GetCartItemCount();
        public Task<IEnumerable<CartDetail>> GetCartProductByUserId(string userId);
        public Task RemoveCartProducts(IEnumerable<CartDetail> cartProduct);
    }
}
