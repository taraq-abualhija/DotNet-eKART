using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X_HIJA_SYSTEM_RAZORpage.Data;
using X_HIJA_SYSTEM_RAZORpage.Models;

namespace X_HIJA_SYSTEM_RAZORpage.Pages.Category
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Categoryy? categoryys { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            categoryys = _db.categories.Find(id);
        }
        public IActionResult OnPost()
        {
            _db.categories.Remove(categoryys);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Succssfully";
            return RedirectToPage("Index");
        }
    }
}
