using Admin.Data;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> AddItem(Guid productId)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                    _db.SaveChanges();
                }
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    var product = _db.Products.Find(productId);
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = 1,
                        Products = product,
                        Image = product.BaseImage,
                        ShoppingCart = cart,
                        UnitPrice = product.Price  // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> AddItem(Guid productId, int qty, string img)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _db.ShoppingCarts.Add(cart);
                    _db.SaveChanges();
                }
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem != null)
                {
                    if (cartItem.Image != img)
                    {
                        var product = _db.Products.Find(productId);
                        cartItem = new CartDetail
                        {
                            ProductId = productId,
                            ShoppingCartId = cart.Id,
                            Quantity = qty,
                            Products = product,
                            Image = img,
                            ShoppingCart = cart,
                            UnitPrice = product.Price  // it is a new line after update
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
                    var product = _db.Products.Find(productId);
                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        Products = product,
                        Image = img,
                        ShoppingCart = cart,
                        UnitPrice = product.Price  // it is a new line after update
                    };
                    _db.CartDetails.Add(cartItem);
                }
                _db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
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
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                // cart detail section
                var cartItem = _db.CartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == bookId);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                else if (cartItem.Quantity == 1)
                    _db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                userId = Guid.NewGuid().ToString();
            var shoppingCart = await _db.ShoppingCarts
                                  .Include(a => a.CartDetails)
                                  .ThenInclude(a => a.Products)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public async Task<int> GetCartItemCount(string userId = "")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            var data = await (from cart in _db.ShoppingCarts
                              join cartDetail in _db.CartDetails
                              on cart.Id equals cartDetail.ShoppingCartId
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }

        //public async Task<bool> DoCheckout()
        //{
        //    using var transaction = _db.Database.BeginTransaction();
        //    try
        //    {
        //        // logic
        //        // move data from cartDetail to order and order detail then we will remove cart detail
        //        var userId = GetUserId();
        //        if (string.IsNullOrEmpty(userId))
        //            throw new Exception("User is not logged-in");
        //        var cart = await GetCart(userId);
        //        if (cart is null)
        //            throw new Exception("Invalid cart");
        //        var cartDetail = _db.CartDetails
        //                            .Where(a => a.ShoppingCartId == cart.Id).ToList();
        //        if (cartDetail.Count == 0)
        //            throw new Exception("Cart is empty");
        //        var order = new Order
        //        {
        //            UserId = userId,
        //            CreateDate = DateTime.UtcNow,
        //            OrderStatusId = 1//pending
        //        };
        //        _db.Orders.Add(order);
        //        _db.SaveChanges();
        //        foreach (var item in cartDetail)
        //        {
        //            var orderDetail = new OrderDetail
        //            {
        //                BookId = item.BookId,
        //                OrderId = order.Id,
        //                Quantity = item.Quantity,
        //                UnitPrice = item.UnitPrice
        //            };
        //            _db.OrderDetails.Add(orderDetail);
        //        }
        //        _db.SaveChanges();

        //        // removing the cartdetails
        //        _db.CartDetails.RemoveRange(cartDetail);
        //        _db.SaveChanges();
        //        transaction.Commit();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
        public async Task<IEnumerable<CartDetail>> GetCartProductByUserId(string userId) // Gets all of the cartProduct data based on the userId from the connected database
        {
            var cart = await GetCart(userId);
            if (cart is null)
                throw new Exception("Invalid cart");
            var cartDetail = _db.CartDetails
                                .Where(a => a.ShoppingCartId == cart.Id).ToList();
            return cartDetail;
        }
        public async Task RemoveCartProducts(IEnumerable<CartDetail> cartProduct) // Deletes a cartProduct of cartItems data from the connected database
        {
            _db.CartDetails.RemoveRange(cartProduct);
            await _db.SaveChangesAsync();
        }
    }
}
