using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class BrandsServices : IBrands
    {
        // Establishes a private connection to a database via dependency injection
        private readonly AltayeeDBContext _context;
        public BrandsServices(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<List<Brands>> GetBrands() // Gets all of the Brands data from the connencted database
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brands> CreateBrand(Brands brands) // Creates a Brands data by saving a Brands object into the connected database
        {
            _context.Entry(brands).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return brands;
        }

        public async Task<Brands> GetBrandById(Guid id)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Brands> UpdateBrand(Guid id, Brands brand)
        {
            var updateBrands = new Brands
            {
                Id = brand.Id,
                NameEn = brand.NameEn,
                ImageUrl = brand.ImageUrl
            };
            _context.Entry(updateBrands).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateBrands;
        }

        public async Task DeleteBrand(Guid id) // Deletes a Brands data based on the id from the connected database
        {
            Brands brands = await GetBrandById(id);
            _context.Entry(brands).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
