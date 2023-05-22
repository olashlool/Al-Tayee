using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Admin.Models.Interface;

namespace User.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private readonly IWishlistRepository _wishlistRepo;

        public WishListController(IWishlistRepository wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }
        public async Task<IActionResult> AddItem(Guid productId, int qty = 1, int redirect = 1)
        {
            var cartCount = await _wishlistRepo.AddItem(productId);
            if (redirect == 0)
                return Ok(cartCount);
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> AddItemWithQtyAndImage(Guid productId, int qty, string image)
        {
            if (qty != 0 && image != null)
            {
                var cartCount = await _wishlistRepo.AddItemWithQty(productId, qty, image);
            }
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            var cartCount = await _wishlistRepo.RemoveItem(productId);
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> index()
        {
            var cart = await _wishlistRepo.GetUserWishlist();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _wishlistRepo.GetWishlistItemCount();
            return Ok(cartItem);
        }
    }
}
