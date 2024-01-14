using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X_HIJA_SYSTEM_RAZORpage.Data;
using X_HIJA_SYSTEM_RAZORpage.Models;

namespace X_HIJA_SYSTEM_RAZORpage.Pages.Category
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Categoryy categoryys { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            _db.categories.Add(categoryys);
            _db.SaveChanges();
            TempData["success"] = "Category Created Succssfully";
            return RedirectToPage("Index");
        }
    }
}
