using Microsoft.AspNetCore.Mvc;
using X_HIJA_SYSTEM.Data;
using X_HIJA_SYSTEM.Models;

namespace X_HIJA_SYSTEM.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> Catelist = _context.categories.ToList();
            return View(Catelist);
        }
    }
}
