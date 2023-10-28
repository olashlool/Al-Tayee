namespace Admin.Models.Interface
{
    public interface IImages
    {
        public Task<int> CreateImages(Images Images);
        public Task<int> CreateImageByProductId(Guid id,Images Images);
        public Task<Images> GetImagesById(Guid id);
        public Task<Images> GetImagesByName(string name);
        public Task<List<Images>> GetImages();
        public Task<int> UpdateImages(Guid id, Images category);
        public Task DeleteImages(Guid id);
        public Task<List<Images>> GetImageByProductId(Guid ProductId);
    }
}
