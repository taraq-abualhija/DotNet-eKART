using Ekart.DataAccess.Data;
using Ekart.DataAccess.Repository;
using Ekart.DataAccess.Repository.IRepository;
using Ekart.Models;
using Ekart.Models.ViewModels;
using Ekart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EkartWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private object success;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> prodList = _unitOfWork.product.GetAll(includeProp: "Category").ToList();
            return View(prodList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategorySelectList = _unitOfWork.category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.CatName,
                    Value = u.CatID.ToString()
                });

            ProductVM productVM = new ProductVM()
            {
                CategoryList = CategorySelectList,
                product = new Product()
            };
            if (id == 0 || id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.product = _unitOfWork.product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.product.Id == 0)
                {
                    _unitOfWork.product.Add(productVM.product);
                }
                else
                {
                    _unitOfWork.product.Update(productVM.product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product Created Succssfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CatName,
                    Value = u.CatID.ToString()
                });
                return View(productVM);
            }
        }

        /*   public IActionResult Delete(int? id)
           {
               if (id == null || id == 0)
               {
                   return NotFound();
               }
               Product? product = _unitOfWork.product.GetFirstOrDefault(u => u.Id == id);
               if (product == null)
               {
                   return NotFound();
               }
               return View(product);
           }

           [HttpPost, ActionName("Delete")]
           public IActionResult DeletePOST(int? id)
           {
               Product? product = _unitOfWork.product.GetFirstOrDefault(u => u.Id == id);
               if (product == null)
               {
                   return NotFound(null);
               }

               _unitOfWork.product.Remove(product);
               _unitOfWork.Save();
               TempData["success"] = "Category Deleted Succssfully";
               return RedirectToAction("Index");

           }
   */

        #region API CALLS
        [HttpGet]
        public IActionResult ApiGetAll()
        {
            List<Product> prodList = _unitOfWork.product.GetAll(includeProp: "Category").ToList();
            return Json(new { data = prodList });
        }

        public IActionResult ApiDelete(int? id)
        {
            var productToBeDeleted = _unitOfWork.product.GetFirstOrDefault(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath =
                        Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });
        }
        #endregion
    }


}
