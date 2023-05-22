namespace Admin.Models.Interface
{
    public interface IBrands
    {
        public Task<Brands> CreateBrand(Brands brand);
        public Task<Brands> GetBrandById(Guid id);
        public Task<List<Brands>> GetBrands();
        public Task<Brands> UpdateBrand(Guid id, Brands brand);
        public Task DeleteBrand(Guid id);
    }
}
