namespace Admin.Models.Interface
{
    public interface IAddresses
    {
        public Task<Addresses> CreateAddress(Addresses addresses);
        public Task<Addresses> GetAddressById(int id);
        public Task<List<Addresses>> GetAddressesByUserId();
        public Task<List<Addresses>> GetAddresses();
        public Task<Addresses> UpdateAddress(int id, Addresses addresses);
        public Task DeleteAddress(int id);

    }
}
