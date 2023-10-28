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
        public async Task<int> CreateImageByProductId(Guid productId, Images image)
        {
            int result;

            // Assuming _context is your DbContext instance

            // Retrieve the product entity from the database
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                // Associate the image with the product
                image.Products = product;

                // Add the image entity to the context
                _context.Entry(image).State = EntityState.Added;

                // Save the changes to the database
                result = await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the product with the given ID doesn't exist
                result = 0;
            }

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
        public async Task<Images> GetImagesByName(string name)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.ImageName == name);
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
