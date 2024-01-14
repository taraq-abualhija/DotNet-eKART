using Ekart.DataAccess.Data;
using Ekart.DataAccess.Repository;
using Ekart.DataAccess.Repository.IRepository;
using Ekart.Models;
using Ekart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EkartWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> Catelist = _unitOfWork.category.GetAll().ToList();
            return View(Catelist);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.CatName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "The DisplayOrder cannot exactly match the CategoryName");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Succssfully";
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.category.GetFirstOrDefault(u => u.CatID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.CatName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "The DisplayOrder cannot exactly match the CategoryName");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Succssfully";
                return RedirectToAction("Index");
            }

            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.category.GetFirstOrDefault(u => u.CatID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _unitOfWork.category.GetFirstOrDefault(u => u.CatID == id);
            if (category == null)
            {
                return NotFound(null);
            }

            _unitOfWork.category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Succssfully";
            return RedirectToAction("Index");

        }

    }
}
