using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IContactUs _contactUs;

        public FeedbackController(IContactUs contactUs)
        {
            _contactUs = contactUs;
        }
        public async Task<IActionResult> Index()
        {
            var contact = await _contactUs.GetFeedback();
            return View(contact);
        }
    }
}
