using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class BrandsServices : IBrands
    {
        private readonly AltayeeDBContext _context;

        public BrandsServices(AltayeeDBContext context)
        {
            _context = context;
        }

        public async Task<List<Brands>> GetBrands()
        {
            return await _context.Brands.AsNoTracking().ToListAsync();
        }

        public async Task<Brands> CreateBrand(Brands brand)
        {
            _context.Entry(brand).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return brand;
        }

        public async Task<Brands> GetBrandById(Guid id)
        {
            return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Brands> UpdateBrand(Guid id, Brands updatedBrand)
        {
            var existingBrand = await _context.Brands.FindAsync(id);
            if (existingBrand == null)
            {
                // Handle the case when the brand with the given id is not found
                return null;
            }
            if (updatedBrand.ImageUrl == null)
            {
                updatedBrand.ImageUrl = existingBrand.ImageUrl;
            }
            _context.Entry(existingBrand).CurrentValues.SetValues(updatedBrand);
            await _context.SaveChangesAsync();

            return existingBrand;
        }

        public async Task DeleteBrand(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Entry(brand).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
