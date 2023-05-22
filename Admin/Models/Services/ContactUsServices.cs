using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class ContactUsServices : IContactUs
    {
        private readonly AltayeeDBContext _context;
        public ContactUsServices(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<List<Brands>> GetBrands() // Gets all of the Brands data from the connencted database
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<ContactUs> Create(ContactUs contactUs) // Creates a Brands data by saving a Brands object into the connected database
        {
            _context.Entry(contactUs).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return contactUs;
        }
        public async Task<List<ContactUs>> GetFeedback() // Gets all of the Brands data from the connencted database
        {
            return await _context.ContactUs.ToListAsync();
        }
    }
}
