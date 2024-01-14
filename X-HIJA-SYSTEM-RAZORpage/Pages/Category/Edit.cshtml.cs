using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X_HIJA_SYSTEM_RAZORpage.Data;
using X_HIJA_SYSTEM_RAZORpage.Models;

namespace X_HIJA_SYSTEM_RAZORpage.Pages.Category
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Categoryy? categoryys { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            categoryys = _db.categories.Find(id);
        }
        public IActionResult OnPost()
        {
            _db.categories.Update(categoryys);
            _db.SaveChanges();
            TempData["success"] = "Category Updated Succssfully";
            return RedirectToPage("Index");
        }
    }
}
