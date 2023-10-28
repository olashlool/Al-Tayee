using Admin.Data;
using Admin.Models;
using Admin.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace User.Controllers
{
    [Authorize] // Apply authorization at the controller level
    public class AddressesController : Controller
    {
        private readonly IAddresses _addressesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressesController(IAddresses addressesService, UserManager<ApplicationUser> userManager)
        {
            _addressesService = addressesService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Addresses> listOfAddresses = await _addressesService.GetAddressesByUserId();
            return View(listOfAddresses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Addresses addresses)
        {
            var propertiesToCheck = new List<string> { nameof(Addresses.FaxNumber), nameof(Addresses.Address2) };

            foreach (var propertyName in propertiesToCheck)
            {
                var propertyValue = (string)addresses.GetType().GetProperty(propertyName)?.GetValue(addresses);

                if (string.IsNullOrEmpty(propertyValue))
                {
                    ModelState.Remove(propertyName);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(addresses);
            }
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                addresses.UserId = userId;
                await _addressesService.CreateAddress(addresses);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the address.");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Addresses addresses = await _addressesService.GetAddressById(id);
            if (addresses == null)
            {
                return NotFound();
            }
            return View(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Addresses addresses)
        {
            if (ModelState.IsValid)
            {
                await _addressesService.UpdateAddress(id , addresses);
                return RedirectToAction("Index");
            }
            return View(addresses);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressesService.DeleteAddress(id);
            return RedirectToAction("Index");
        }
    }
}
