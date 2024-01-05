using Admin.Data;
using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<int> AddItem(Guid productId, string img)
        {
            string userId = GetUserId();
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var wishlist = await GetOrCreateWishlist(userId);
                var product = await _db.Products.FindAsync(productId);
                await UpdateWishlistItem(productId, wishlist, 1, img);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var wishlistItemCount = await GetWishlistItemCount();
            return wishlistItemCount;
        }

        public async Task<int> AddItem(Guid productId, int qty, string img)
        {
            string userId = GetUserId();
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var wishlist = await GetOrCreateWishlist(userId);
                await UpdateWishlistItem(productId, wishlist, qty, img);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var wishlistItemCount = await GetWishlistItemCount();
            return wishlistItemCount;
        }

        public async Task<int> RemoveItem(Guid productId , string img)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var wishlist = await GetWishlist(userId);
                if (wishlist == null)
                    throw new Exception("Wishlist not found");

                var wishlistItem = wishlist.WishlistsDetail.Where(a => a.ProductId == productId && a.Image == img).FirstOrDefault();
                if (wishlistItem != null)
                {
                    if (wishlistItem.Quantity > 1)
                        wishlistItem.Quantity -= 1;
                    else
                        _db.WishlistDetail.Remove(wishlistItem);
                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var wishlistItemCount = await GetWishlistItemCount();
            return wishlistItemCount;
        }

        public async Task<Wishlist> GetUserWishlist()
        {
            string userId = GetUserId();
            if (userId == null)
            {
                // Handle the case where userId is null (e.g., redirect to login or show an error message)
                // For now, let's return an empty wishlist or some default response
                var emptyWishlist = new Wishlist(); // You need to replace this with your actual Wishlist class
                return emptyWishlist;
            }
            else
            {
                var wishlist = await GetOrCreateWishlist(userId);
                return wishlist;
            }
        }

        public async Task<IEnumerable<WishlistDetail>> GetWishlistProductByUserId(string userId)
        {
            var wishlist = await GetWishlist(userId);
            if (wishlist == null)
                throw new Exception("Invalid wishlist");

            return wishlist.WishlistsDetail.ToList();
        }

        public async Task RemoveWishlistProducts(IEnumerable<WishlistDetail> wishlistProducts)
        {
            _db.WishlistDetail.RemoveRange(wishlistProducts);
            await _db.SaveChangesAsync();
        }

        public async Task<Wishlist> GetWishlist(string userId)
        {
            return await _db.Wishlists.Include(w => w.WishlistsDetail)
                .ThenInclude(d => d.Products)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<int> GetWishlistItemCount()
        {
            string userId = GetUserId();
            var wishlist = await GetWishlist(userId);
            return wishlist?.WishlistsDetail.Sum(d => d.Quantity) ?? 0;
        }

        private async Task<Wishlist> GetOrCreateWishlist(string userId)
        {
            var wishlist = await GetWishlist(userId);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId
                };
                _db.Wishlists.Add(wishlist);
                await _db.SaveChangesAsync();
            }

            return wishlist;
        }

        private async Task UpdateWishlistItem(Guid productId, Wishlist wishlist, int qty, string img)
        {
            //var wishlistItem = wishlist.WishlistsDetail.FirstOrDefault(a => a.ProductId == productId);
            var wishlistItem = wishlist.WishlistsDetail.Where(x => x.Image == img).FirstOrDefault();
            if (wishlistItem != null)
            {
                if (wishlistItem.Image != img)
                {
                    wishlistItem = new WishlistDetail
                    {
                        ProductId = productId,
                        WishlistId = wishlist.Id,
                        Quantity = qty,
                        Image = img == null ? wishlistItem.Image : img,
                        Products = await _db.Products.FindAsync(productId),
                        Wishlist = wishlist,
                        UnitPrice = (await _db.Products.FindAsync(productId))?.Price ?? 0,
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
                var product = await _db.Products.FindAsync(productId);
                wishlistItem = new WishlistDetail
                {
                    ProductId = productId,
                    WishlistId = wishlist.Id,
                    Quantity = qty,
                    Products = product,
                    Image = img == null ? product?.BaseImage : img,
                    Wishlist = wishlist,
                    UnitPrice = product?.Price ?? 0,
                };
                _db.WishlistDetail.Add(wishlistItem);
            }

            await _db.SaveChangesAsync();
        }



        private string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
























//using Admin.Data;
//using Admin.Models;
//using Admin.Models.Interface;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Admin.Models.Services
//{
//    public class WishlistRepository : IWishlistRepository
//    {
//        private readonly AltayeeDBContext _db;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public WishlistRepository(AltayeeDBContext db, IHttpContextAccessor httpContextAccessor,
//            UserManager<ApplicationUser> userManager)
//        {
//            _db = db;
//            _userManager = userManager;
//            _httpContextAccessor = httpContextAccessor;
//        }
//        public async Task<int> AddItem(Guid productId)
//        {
//            string userId = GetUserId();
//            using var transaction = _db.Database.BeginTransaction();
//            try
//            {
//                if (string.IsNullOrEmpty(userId))
//                    throw new Exception("user is not logged-in");
//                var wishlist = await GetWishlist(userId);
//                if (wishlist == null)
//                {
//                    wishlist = new Wishlist
//                    {

//                        UserId = userId
//                    };
//                    _db.Wishlists.Add(wishlist);
//                    _db.SaveChanges();
//                }
//                // cart detail section
//                var wishlistItem = _db.WishlistDetail
//                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == productId);
//                if (wishlistItem != null)
//                {
//                    wishlistItem.Quantity += 1;
//                }
//                else
//                {
//                    var product = _db.Products.Find(productId);
//                    wishlistItem = new WishlistDetail
//                    {
//                        ProductId = productId,
//                        WishlistId = wishlist.Id,
//                        Quantity = 1,
//                        Products = product,
//                        Image = product.BaseImage,
//                        Wishlist = wishlist,
//                        UnitPrice = product.Price  // it is a new line after update
//                    };
//                    _db.WishlistDetail.Add(wishlistItem);
//                }
//                _db.SaveChanges();
//                transaction.Commit();
//            }
//            catch (Exception ex)
//            {
//            }
//            var cartItemCount = await GetWishlistItemCount();
//            return cartItemCount;
//        }

//        public async Task<int> AddItemWithQty(Guid productId, int qty, string img)
//        {
//            string userId = GetUserId();
//            using var transaction = _db.Database.BeginTransaction();
//            try
//            {
//                if (string.IsNullOrEmpty(userId))
//                    throw new Exception("user is not logged-in");
//                var wishlist = await GetWishlist(userId);
//                if (wishlist == null)
//                {
//                    wishlist = new Wishlist
//                    {

//                        UserId = userId
//                    };
//                    _db.Wishlists.Add(wishlist);
//                    _db.SaveChanges();
//                }
//                // cart detail section
//                var wishlistItem = _db.WishlistDetail
//                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == productId);
//                if (wishlistItem != null)
//                {
//                    var product = _db.Products.Find(productId);
//                    if (wishlistItem.Products.BaseImage != img)
//                    {
//                        wishlistItem = new WishlistDetail
//                        {
//                            ProductId = productId,
//                            WishlistId = wishlist.Id,
//                            Quantity = qty,
//                            Image = img,
//                            Products = product,
//                            Wishlist = wishlist,
//                            UnitPrice = product.Price,  // it is a new line after update
//                        };
//                        _db.WishlistDetail.Add(wishlistItem);
//                    }
//                    else
//                    {
//                        wishlistItem.Quantity += qty;
//                    }
//                }
//                else
//                {
//                    var product = _db.Products.Find(productId);
//                    wishlistItem = new WishlistDetail
//                    {
//                        ProductId = productId,
//                        WishlistId = wishlist.Id,
//                        Quantity = qty,
//                        Products = product,
//                        Image = img,
//                        Wishlist = wishlist,
//                        UnitPrice = product.Price  // it is a new line after update
//                    };
//                    _db.WishlistDetail.Add(wishlistItem);
//                }
//                _db.SaveChanges();
//                transaction.Commit();
//            }
//            catch (Exception ex)
//            {
//            }
//            var cartItemCount = await GetWishlistItemCount();
//            return cartItemCount;
//        }
//        public async Task<int> RemoveItem(Guid bookId)
//        {
//            //using var transaction = _db.Database.BeginTransaction();
//            string userId = GetUserId();
//            try
//            {
//                if (string.IsNullOrEmpty(userId))
//                    throw new Exception("user is not logged-in");
//                var wishlist = await GetWishlist(userId);
//                if (wishlist is null)
//                    throw new Exception("Invalid cart");
//                // cart detail section
//                var wishlistItem = _db.WishlistDetail
//                                  .FirstOrDefault(a => a.WishlistId == wishlist.Id && a.ProductId == bookId);
//                if (wishlistItem is null)
//                    throw new Exception("Not items in cart");
//                else if (wishlistItem.Quantity == 1)
//                    _db.WishlistDetail.Remove(wishlistItem);
//                else
//                    wishlistItem.Quantity = wishlistItem.Quantity - 1;
//                _db.SaveChanges();
//            }
//            catch (Exception ex)
//            {

//            }
//            var cartItemCount = await GetWishlistItemCount();
//            return cartItemCount;
//        }

//        public async Task<Wishlist> GetUserWishlist()
//        {
//            var userId = GetUserId();
//            if (userId == null)
//                userId = Guid.NewGuid().ToString();
//            var wishlist = await _db.Wishlists
//                                  .Include(a => a.WishlistsDetail)
//                                  .ThenInclude(a => a.Products)
//                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
//            return wishlist;

//        }
//        public async Task<Wishlist> GetWishlist(string userId)
//        {
//            var wishlists = await _db.Wishlists.FirstOrDefaultAsync(x => x.UserId == userId);
//            return wishlists;
//        }

//        public async Task<int> GetWishlistItemCount()
//        {
//            string userId = GetUserId();
//            var wishlist = await GetWishlist(userId);
//            return wishlist?.WishlistsDetail.Sum(d => d.Quantity) ?? 0;

//            //var data = await (from wishlists in _db.Wishlists
//            //                  join wishlistDetail in _db.WishlistDetail
//            //                  on wishlists.Id equals wishlistDetail.WishlistId
//            //                  select new { wishlistDetail.Id }
//            //            ).ToListAsync();
//            //return data.Count;
//        }
//        private string GetUserId()
//        {
//            var principal = _httpContextAccessor.HttpContext.User;
//            string userId = _userManager.GetUserId(principal);
//            return userId;
//        }
//    }
//}
