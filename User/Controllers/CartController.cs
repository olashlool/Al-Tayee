using Admin.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        [Authorize]
        public async Task<IActionResult> AddItem(Guid productId, string img, int qty = 1, int redirect = 1)
        {
            var cartCount = await _cartRepo.AddItem(productId, img);
            if (redirect == 0)
                return Ok(cartCount);
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        [Authorize]
        public async Task<IActionResult> AddItemWithQtyAndImage(Guid productId, int qty, string image )
        {
            if (qty != 0 && image != null)
            {
                var cartCount = await _cartRepo.AddItem(productId, qty, image);
            }
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> RemoveItem(Guid productId, string img)
        {
            var cartCount = await _cartRepo.RemoveItem(productId, img);
            string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> index()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }
    }
}
