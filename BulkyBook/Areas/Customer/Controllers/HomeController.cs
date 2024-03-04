using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookModel.Models;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public IActionResult Index()
        {
            //Get All Product Details and also include the Details of catgeory which is releted to Product
            IEnumerable<ProductModel> productlist = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productlist);
        }
        public IActionResult ProductDetails(int productid)
        {
            ShoppingCart cart = new()
            {
                //Get Product from id 
                Product = _unitOfWork.Product.Get(u => u.ProductID == productid, includeProperties: "Category"),
                ProductCount = 1,
                ProductID = productid
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ProductDetails(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            // Get Shoppin Product
            ShoppingCart ShoppingProductFromDb = _unitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId == userId && u.ProductID== shoppingCart.ProductID);

            if(ShoppingProductFromDb != null)
            {
                //Update Shopping Cart
                ShoppingProductFromDb.ProductCount += shoppingCart.ProductCount;
                _unitOfWork.ShoppingCart.Update(ShoppingProductFromDb);
                _unitOfWork.Save();
            }
            else
            {
                //Add Shopping Cart
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(StaticData.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
           
            TempData["Success"] = "Cart updated Successfully";
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
 