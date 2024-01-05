namespace Admin.Models.Interface
{
    public interface IGridImage
    {
        public Task<GridImage> CreateGridImage(GridImage GridImage);
        public Task<List<GridImage>> GetGridImages();
        public Task<GridImage> UpdateGridImage(int id, GridImage GridImage);
        public Task DeleteGridImage(int id);

    }
}
