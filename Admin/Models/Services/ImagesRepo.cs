using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Admin.Models.Services
{
    public class ImagesRepo : IImages
    {
        private readonly AltayeeDBContext _context;
        public ImagesRepo(AltayeeDBContext context)
        {
            _context = context;
        }
        public async Task<int> CreateImages(Images Images)
        {
            int result;
            _context.Entry(Images).State = EntityState.Added;
            result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task DeleteImages(Guid id)
        {
            Images images = await GetImagesById(id);
            _context.Entry(images).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Images>> GetImageByProductId(Guid ProductId)
        {
            return await _context.Images.Where(x => x.ProductsId == ProductId).ToListAsync();
        }

        public async Task<List<Images>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Images> GetImagesById(Guid id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateImages(Guid id, Images Image)
        {
            int result;
            var images = new Images
            {
                Id = Image.Id,
                ImageName = Image.ImageName,
                ProductsId = Image.ProductsId
            };
            _context.Entry(images).State = EntityState.Modified;
            result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
