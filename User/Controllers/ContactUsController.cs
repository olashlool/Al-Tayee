using Admin.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Admin.Models;
using System;
using System.Text.RegularExpressions;

namespace User.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUs _contactUs;
        string culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

        public ContactUsController(IContactUs contactUs)
        {
            _contactUs = contactUs;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                await _contactUs.Create(contactUs);
                ViewBag.IsSuccess = true;
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
