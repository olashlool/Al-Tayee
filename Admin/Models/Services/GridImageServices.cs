using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class GridImageServices : IGridImage
    {
        private readonly AltayeeDBContext _context;

        public GridImageServices(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<GridImage> CreateGridImage(GridImage gridImage)
        {
            _context.Entry(gridImage).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return gridImage;
        }

        public async Task DeleteGridImage(int id)
        {
            var gridImage = await _context.GridImage.FindAsync(id);
            if (gridImage != null)
            {
                _context.Entry(gridImage).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GridImage>> GetGridImages()
        {
            return await _context.GridImage.AsNoTracking().ToListAsync();
        }

        public async Task<GridImage> UpdateGridImage(int id, GridImage gridImage)
        {
            var existinggridImage = await _context.GridImage.FindAsync(id);
            if (existinggridImage == null)
            {
                // Handle the case when the brand with the given id is not found
                return null;
            }
            if (gridImage.ImageUrl == null)
            {
                gridImage.ImageUrl = existinggridImage.ImageUrl;
            }
            _context.Entry(existinggridImage).CurrentValues.SetValues(gridImage);
            await _context.SaveChangesAsync();

            return existinggridImage;
        }
    }
}
