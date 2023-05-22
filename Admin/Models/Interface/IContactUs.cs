namespace Admin.Models.Interface
{
    public interface IContactUs
    {
        public Task<ContactUs> Create(ContactUs contactUs);
        public Task<List<ContactUs>> GetFeedback();
    }
}
