using Admin.Data;
using Admin.Models.Exceptions;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Admin.Models.Services
{
    public class AddressesServices : IAddresses
    {
        //Establishes a private connection to a database via dependency injection
        private readonly AltayeeDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressesServices(AltayeeDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Addresses> CreateAddress(Addresses addresses) // Creates a Addresses data by saving a Addresses object into the connected database
        {
            _dbContext.Entry(addresses).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return addresses;
        }
        public async Task<List<Addresses>> GetAddresses() // Gets all of the Addresses data from the connencted database
        {
            return await _dbContext.Addresses.ToListAsync();
        }
        public async Task<Addresses> GetAddressById(int id)
        {
            return await _dbContext.Addresses.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Addresses>> GetAddressesByUserId()
        {
            var userId = GetUserId();
            return await _dbContext.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }
        public async Task<Addresses> UpdateAddress(int id, Addresses addresses)
        {
            var existingAddress = await GetAddressById(id);
            if (existingAddress == null)
            {
                throw new NotFoundException("Address not found.");
            }

            existingAddress.FirstName = addresses.FirstName;
            existingAddress.LastName = addresses.LastName;
            existingAddress.Email = addresses.Email;
            existingAddress.PhoneNumber = addresses.PhoneNumber;
            existingAddress.FaxNumber = addresses.FaxNumber;
            existingAddress.Address1 = addresses.Address1;
            existingAddress.Address2 = addresses.Address2;
            existingAddress.City = addresses.City;
            existingAddress.Country = addresses.Country;

            await _dbContext.SaveChangesAsync();
            return existingAddress;
        }
        public async Task DeleteAddress(int id) // Deletes a Addresses data based on the id from the connected database
        {
            Addresses addresses = await GetAddressById(id);
            if (addresses == null)
            {
                throw new NotFoundException("Address not found.");
            }
            _dbContext.Entry(addresses).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
        private string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
