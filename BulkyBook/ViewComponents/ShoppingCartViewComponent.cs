using BulkyBook.Areas.Customer.Controllers;
using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookUtility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBook.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                if (HttpContext.Session.GetInt32(StaticData.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(StaticData.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }
                return View(HttpContext.Session.GetInt32(StaticData.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
