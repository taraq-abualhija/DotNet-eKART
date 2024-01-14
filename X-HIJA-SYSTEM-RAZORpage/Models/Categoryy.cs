using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace X_HIJA_SYSTEM_RAZORpage.Models
{
    public class Categoryy
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
