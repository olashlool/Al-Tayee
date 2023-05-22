namespace Admin.Models.Interface
{
    public interface ITypes
    {
        public Task<int> CreateType(Types Type);
        public Task<Types> GetTypeById(Guid id);
        public Task<List<Types>> GetTypes();
        public Task<int> UpdateType(Guid id, Types category);
        public Task DeleteType(Guid id);
        public Task<List<Types>> GetTypeByProductId(Guid ProductId);
    }
}
