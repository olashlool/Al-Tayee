using Admin.Data;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Admin.Models.Services
{
    public class CartRepository : ICartRepository
    {
        private readonly AltayeeDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(AltayeeDBContext db, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddItem(Guid productId, string img)
        {
            string userId = GetUserId();

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var cart = await GetOrCreateCart(userId);

                var cartItem = cart.CartDetails.FirstOrDefault(a => a.ProductId == productId && a.Image == img);
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    var product = await _db.Products.FindAsync(productId);
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = 1,
                        Products = product,
                        Image = product.BaseImage,
                        ShoppingCart = cart,
                        UnitPrice = product.Price
                    };
                    _db.CartDetails.Add(cartItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var cartItemCount = await GetCartItemCount();
            return cartItemCount;
        }
        public async Task<int> AddItem(Guid productId, int qty, string img)
        {
            string userId = GetUserId();

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var cart = await GetOrCreateCart(userId);

               // var cartItem = cart.CartDetails.FirstOrDefault(a => a.ProductId == productId);
                var cartItem = cart.CartDetails.Where(x => x.Image == img).FirstOrDefault();

                if (cartItem != null)
                {
                    if (cartItem.Image != img)
                    {
                        var product = await _db.Products.FindAsync(productId);
                        cartItem = new CartDetail
                        {
                            ProductId = productId,
                            ShoppingCartId = cart.Id,
                            Quantity = qty,
                            Products = product,
                            Image = img,
                            ShoppingCart = cart,
                            UnitPrice = product.Price
                        };
                        _db.CartDetails.Add(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity += qty;
                    }
                }
                else
                {
                    var product = await _db.Products.FindAsync(productId);
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        Products = product,
                        Image = img,
                        ShoppingCart = cart,
                        UnitPrice = product.Price
                    };
                    _db.CartDetails.Add(cartItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var cartItemCount = await GetCartItemCount();
            return cartItemCount;
        }
        public async Task<int> RemoveItem(Guid bookId, string img)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");

                var cart = await GetCart(userId);
                if (cart == null)
                    throw new Exception("Cart not found");

                //var cartItem = cart.CartDetails.FirstOrDefault(a => a.ProductId == bookId);
                var cartItem = cart.CartDetails.Where(a => a.ProductId == bookId && a.Image == img).FirstOrDefault();

                if (cartItem != null)
                {
                    if (cartItem.Quantity > 1)
                        cartItem.Quantity -= 1;
                    else
                        _db.CartDetails.Remove(cartItem);
                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log or throw)
            }

            var cartItemCount = await GetCartItemCount();
            return cartItemCount;
        }
        public async Task<ShoppingCart> GetUserCart()
        {
            string userId = GetUserId();
            if (userId == null)
            {
                // Handle the case where userId is null (e.g., redirect to login or show an error message)
                // For now, let's return an empty cart or some default response
                var emptyCart = new ShoppingCart(); // You need to replace this with your actual Cart class
                return emptyCart;
            }
            else
            {
                var cart = await GetOrCreateCart(userId);
                return cart;
            }
        }
        public async Task<IEnumerable<CartDetail>> GetCartProductByUserId(string userId)
        {
            var cart = await GetCart(userId);
            if (cart is null)
                throw new Exception("Invalid cart");
            return cart.CartDetails.ToList();
        }
        public async Task RemoveCartProducts(IEnumerable<CartDetail> cartProduct)
        {
            _db.CartDetails.RemoveRange(cartProduct);
            await _db.SaveChangesAsync();
        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            return await _db.ShoppingCarts.Include(c => c.CartDetails)
                .ThenInclude(d => d.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<int> GetCartItemCount()
        {
            string userId = GetUserId();
            var cart = await GetCart(userId);
            return cart?.CartDetails.Sum(d => d.Quantity) ?? 0;
        }
        private async Task<ShoppingCart> GetOrCreateCart(string userId)
        {
            var cart = await GetCart(userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId
                };
                _db.ShoppingCarts.Add(cart);
                await _db.SaveChangesAsync();
            }

            return cart;
        }
        private string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
