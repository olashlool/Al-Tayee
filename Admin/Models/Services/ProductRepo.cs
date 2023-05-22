using Admin.Data;
using Admin.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Admin.Models.Services
{
    public class ProductRepo : IProducts
    {
        private readonly AltayeeDBContext _context;
        public ProductRepo(AltayeeDBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(Products Product)
        {
            int result;
            _context.Entry(Product).State = EntityState.Added;
            result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteProduct(Guid id)
        {
            Products products = await GetProductById(id);
            _context.Entry(products).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Products>> GetFeaturedProduct()
        {
            var products = await GetProducts();
            var fearterdProducts = products.Where(x => x.IsFeatured == true).ToList();
            return fearterdProducts;
        }

        public async Task<Products> GetProductById(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Products>> GetProductByBrand(Guid brandId)
        {
            var products = await GetProducts();
            var productByBrand = products.Where(x => x.BrandsId == brandId).ToList();
            return productByBrand;
        }
        public async Task<List<Products>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> UpdateProduct(Guid id, Products products)
        {
            int result;
            var product = new Products
            {
                Id = products.Id,
                NameEn = products.NameEn,
                DescriptionEn = products.DescriptionEn,
                BaseImage = products.BaseImage,
                AltImage = products.AltImage,
                BrandsId = products.BrandsId,
                IsFeatured = products.IsFeatured,
                Size = products.Size,
                Price = products.Price,
            };
            _context.Entry(product).State = EntityState.Modified;
            result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
