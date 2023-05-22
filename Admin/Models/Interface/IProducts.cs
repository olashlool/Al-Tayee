namespace Admin.Models.Interface
{
    public interface IProducts
    {
        public Task<int> CreateProduct(Products Product);
        public Task<Products> GetProductById(Guid id);
        public Task<List<Products>> GetProductByBrand(Guid brand);
        public Task<List<Products>> GetProducts();
        public Task<int> UpdateProduct(Guid id, Products category);
        public Task DeleteProduct(Guid id);
        public Task<List<Products>> GetFeaturedProduct();
    }
}
