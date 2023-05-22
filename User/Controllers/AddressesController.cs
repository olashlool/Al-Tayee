using Admin.Data;
using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace User.Controllers
{
    public class AddressesController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly IAddresses _Addresses;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AddressesController(IWebHostEnvironment environment, IAddresses addresses, IConfiguration configuration, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _configuration = configuration;
            _Addresses = addresses;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Addresses> listOfAddresses = await _Addresses.GetAddresses();
            listOfAddresses = listOfAddresses.Where(x => x.UserId == userId);
            return View(listOfAddresses);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Addresses addresses)
        {
            if (ModelState.IsValid)
            {
                await _Addresses.CreateAddress(addresses);
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Int32 id)
        {

            Addresses addresses = await _Addresses.GetAddressById(id);
            return View(addresses);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Int32 id , Addresses addresses)
        {
            if (ModelState.IsValid)
            {
                await _Addresses.UpdateAddress(addresses.Id, addresses);
                return RedirectToAction("Index");
            }
            return View(addresses);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _Addresses.DeleteAddress(id);
            return RedirectToAction("Index");
        }
    }
}
