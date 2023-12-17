using System.ComponentModel.DataAnnotations;
namespace X_HIJA_SYSTEM.Models
{
    public class Category
    {
        [Key]
        public int CatID { get; set; }
        [Required]
        public string? CatName { get; set; }
        public string? DisplayOrder { get; set; }

    }
}
