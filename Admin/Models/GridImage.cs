using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    public class GridImage
    {
        public int Id { get; set; }
        [NotMapped]
        public IList<IFormFile> ImageUpload { get; set; }
        public string ImageUrl { get; set; }
    }
}
