using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
    
namespace Admin.Models
{
    public class Brands
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is rquried")]
        public string NameEn { get; set; }
        [Required(ErrorMessage = "This field is rquried")]
        public string NameAr { get; set; }

        public string ImageUrl { get; set; }
    }
}
