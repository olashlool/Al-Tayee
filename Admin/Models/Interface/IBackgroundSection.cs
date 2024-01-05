namespace Admin.Models.Interface
{
    public interface IBackgroundSection
    {
        public Task<BackgroundSection> CreateBackgroundSection(BackgroundSection backgroundSection);
        public Task<BackgroundSection> GetBackgroundSectionById(int id);
        public Task<List<BackgroundSection>> GetBackgroundSection();
        public Task<BackgroundSection> UpdateBackgroundSection(int id, BackgroundSection backgroundSection);
        public Task DeleteBackgroundSection(int id);

    }
}
