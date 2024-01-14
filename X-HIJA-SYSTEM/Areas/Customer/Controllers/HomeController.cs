using Ekart.DataAccess.Repository;
using Ekart.DataAccess.Repository.IRepository;
using Ekart.Models;
using Ekart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace EkartWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.product.GetAll(includeProp: "Category");
            return View(products);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = _unitOfWork.product.GetFirstOrDefault(u => u.Id == productId, includeProp: "Category"),
                Count = 1,
                ProductId = productId
            };

            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            obj.ApplicationUserID = userID;
            ShoppingCart isIncludeInDB = _unitOfWork.shoppingCart.GetFirstOrDefault(u => u.ApplicationUserID == userID && u.ProductId == obj.ProductId);
            if (isIncludeInDB != null)
            {
                // Shopping cart Exist
                isIncludeInDB.Count = obj.Count;
                _unitOfWork.shoppingCart.Update(isIncludeInDB);
                _unitOfWork.Save();
                TempData["success"] = "Cart Updated Successfully";
            }
            else
            {
                // add cart record
                _unitOfWork.shoppingCart.Add(obj);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.shoppingCart.GetAll(u => u.ApplicationUserID == userID).Count());
                TempData["success"] = "The Item added To Cart Successfully";
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
