using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Ekart.Models
{
    public class Category
    {
        [Key]
        public int CatID { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string? CatName { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 200, ErrorMessage = "The field Display Order must be between 1-200")]
        public int? DisplayOrder { get; set; }

    }
}
