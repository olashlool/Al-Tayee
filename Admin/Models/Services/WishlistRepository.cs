using Admin.Data;
using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AltayeeDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishlistRepository(AltayeeDBContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(Guid productId)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var wishlist = await GetWishlist(userId);
                if (wishlist == null)
                {
                    wishlist = new Wishlist
                    {

                        UserId = userId
                    };
                    _db.Wishlists.Add(wishlist);
                    _db.SaveChanges();
                }
                // cart detail section
                var wishlistItem = _db.WishlistDetail
                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == productId);
                if (wishlistItem != null)
                {
                    wishlistItem.Quantity += 1;
                }
                else
                {
                    var product = _db.Products.Find(productId);
                    wishlistItem = new WishlistDetail
                    {
                        ProductId = productId,
                        WishlistId = wishlist.Id,
                        Quantity = 1,
                        Products = product,
                        Image = product.BaseImage,
                        Wishlist = wishlist,
                        UnitPrice = product.Price  // it is a new line after update
                    };
                    _db.WishlistDetail.Add(wishlistItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetWishlistItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> AddItemWithQty(Guid productId, int qty, string img)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var wishlist = await GetWishlist(userId);
                if (wishlist == null)
                {
                    wishlist = new Wishlist
                    {

                        UserId = userId
                    };
                    _db.Wishlists.Add(wishlist);
                    _db.SaveChanges();
                }
                // cart detail section
                var wishlistItem = _db.WishlistDetail
                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == productId);
                if (wishlistItem != null)
                {
                    var product = _db.Products.Find(productId);
                    if (wishlistItem.Products.BaseImage != img)
                    {
                        wishlistItem = new WishlistDetail
                        {
                            ProductId = productId,
                            WishlistId = wishlist.Id,
                            Quantity = qty,
                            Image = img,
                            Products = product,
                            Wishlist = wishlist,
                            UnitPrice = product.Price,  // it is a new line after update
                        };
                        _db.WishlistDetail.Add(wishlistItem);
                    }
                    else
                    {
                        wishlistItem.Quantity += qty;
                    }
                }
                else
                {
                    var product = _db.Products.Find(productId);
                    wishlistItem = new WishlistDetail
                    {
                        ProductId = productId,
                        WishlistId = wishlist.Id,
                        Quantity = qty,
                        Products = product,
                        Image = img,
                        Wishlist = wishlist,
                        UnitPrice = product.Price  // it is a new line after update
                    };
                    _db.WishlistDetail.Add(wishlistItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetWishlistItemCount(userId);
            return cartItemCount;
        }
        public async Task<int> RemoveItem(Guid bookId)
        {
            //using var transaction = _db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var wishlist = await GetWishlist(userId);
                if (wishlist is null)
                    throw new Exception("Invalid cart");
                // cart detail section
                var wishlistItem = _db.WishlistDetail
                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == bookId);
                if (wishlistItem is null)
                    throw new Exception("Not items in cart");
                else if (wishlistItem.Quantity == 1)
                    _db.WishlistDetail.Remove(wishlistItem);
                else
                    wishlistItem.Quantity = wishlistItem.Quantity - 1;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetWishlistItemCount(userId);
            return cartItemCount;
        }

        public async Task<Wishlist> GetUserWishlist()
        {
            var userId = GetUserId();
            if (userId == null)
                userId = Guid.NewGuid().ToString();
            var wishlist = await _db.Wishlists
                                  .Include(a => a.WishlistsDetail)
                                  .ThenInclude(a => a.Products)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return wishlist;

        }
        public async Task<Wishlist> GetWishlist(string userId)
        {
            var wishlists = await _db.Wishlists.FirstOrDefaultAsync(x => x.UserId == userId);
            return wishlists;
        }

        public async Task<int> GetWishlistItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from wishlists in _db.Wishlists
                              join wishlistDetail in _db.WishlistDetail
                              on wishlists.Id equals wishlistDetail.WishlistId
                              select new { wishlistDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
