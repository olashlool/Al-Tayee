using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class BackgroundSectionServices : IBackgroundSection
    {
        private readonly AltayeeDBContext _context;

        public BackgroundSectionServices(AltayeeDBContext context)
        {
            _context = context;
        }

        public async Task<List<BackgroundSection>> GetBackgroundSection()
        {
            return await _context.BackgroundSection.AsNoTracking().ToListAsync();
        }

        public async Task<BackgroundSection> CreateBackgroundSection(BackgroundSection backgroundSection)
        {
            _context.Entry(backgroundSection).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return backgroundSection;
        }

        public async Task<BackgroundSection> GetBackgroundSectionById(int id)
        {
            return await _context.BackgroundSection.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BackgroundSection> UpdateBackgroundSection(int id, BackgroundSection updatedBackgroundSection)
        {
            var existingBackgroundSection = await _context.BackgroundSection.FindAsync(id);
            if (existingBackgroundSection == null)
            {
                // Handle the case when the brand with the given id is not found
                return null;
            }
            if (updatedBackgroundSection.ImageUrl == null)
            {
                updatedBackgroundSection.ImageUrl = existingBackgroundSection.ImageUrl;
            }
            _context.Entry(existingBackgroundSection).CurrentValues.SetValues(updatedBackgroundSection);
            await _context.SaveChangesAsync();

            return existingBackgroundSection;
        }

        public async Task DeleteBackgroundSection(int id)
        {
            var backgroundSection = await _context.BackgroundSection.FindAsync(id);
            if (backgroundSection != null)
            {
                _context.Entry(backgroundSection).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

    }
}
