using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class AddressesServices : IAddresses
    {
        //Establishes a private connection to a database via dependency injection
        private readonly AltayeeDBContext _context;
        public AddressesServices(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<List<Addresses>> GetAddresses() // Gets all of the Addresses data from the connencted database
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Addresses> CreateAddress(Addresses addresses) // Creates a Addresses data by saving a Addresses object into the connected database
        {
            _context.Entry(addresses).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return addresses;
        }

        public async Task<Addresses> GetAddressById(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Addresses> UpdateAddress(int id, Addresses addresses)
        {
            var updateAddresses = new Addresses
            {
                Id = addresses.Id,
                FirstName = addresses.FirstName,
                LastName = addresses.LastName,
                Email = addresses.Email,
                PhoneNumber = addresses.PhoneNumber,
                FaxNumber = addresses.FaxNumber,
                Address1 = addresses.Address1,
                Address2 = addresses.Address2,
                City = addresses.City,
                Country = addresses.Country,
            };
            _context.Entry(addresses).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateAddresses;
        }

        public async Task DeleteAddress(int id) // Deletes a Addresses data based on the id from the connected database
        {
            Addresses addresses = await GetAddressById(id);
            _context.Entry(addresses).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
