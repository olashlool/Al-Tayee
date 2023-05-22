using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Models.Components
{
    public class SmallCartViewComponent : ViewComponent
    {
        private readonly ICartRepository _cartRepo;

        public SmallCartViewComponent(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
    }
}
