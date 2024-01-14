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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Company> prodList = _unitOfWork.company.GetAll().ToList();
            return View(prodList);
        }

        public IActionResult Upsert(int? id)
        {

            if (id == 0 || id == null)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.company.GetFirstOrDefault(u => u.Id == id);
                return View(companyObj);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _unitOfWork.company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.company.Update(companyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Created Succssfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
        }



        #region API CALLS
        [HttpGet]
        public IActionResult ApiGetAll()
        {
            List<Company> companies = _unitOfWork.company.GetAll().ToList();
            return Json(new { data = companies });
        }

        public IActionResult ApiDelete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.company.GetFirstOrDefault(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });
        }
        #endregion
    }

}
