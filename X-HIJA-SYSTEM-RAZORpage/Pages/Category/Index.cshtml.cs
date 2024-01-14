using Microsoft.AspNetCore.Mvc.RazorPages;
using X_HIJA_SYSTEM_RAZORpage.Data;
using X_HIJA_SYSTEM_RAZORpage.Models;


namespace X_HIJA_SYSTEM_RAZORpage.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Categoryy> categoryys { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            categoryys = _db.categories.ToList();
        }
    }
}
