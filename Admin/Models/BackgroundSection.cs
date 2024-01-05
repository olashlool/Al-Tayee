using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class BackgroundSection
    {
        public int Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string ImageUrl { get; set; }
    }
}
