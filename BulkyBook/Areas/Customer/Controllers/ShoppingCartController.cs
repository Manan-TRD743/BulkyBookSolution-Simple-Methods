using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookModel.ViewModel;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var Cart in ShoppingCartVM.ShoppingCartList)
            {
                Cart.TotalPrice = GetPriceBasedOnQuantity(Cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (Cart.TotalPrice * Cart.ProductCount);
            }

            return View(ShoppingCartVM);
        }

        #region Add Item
        public IActionResult PlusItem(int cartID)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.CartId == cartID);
            cartFromDb.ProductCount += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Remove item
        public IActionResult MinusItem(int cartID)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.CartId == cartID);
            if (cartFromDb.ProductCount <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                HttpContext.Session.SetInt32(StaticData.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count()-1);
            }
            else
            {
                cartFromDb.ProductCount -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Item
        public IActionResult Remove(int cartID)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.CartId == cartID);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(StaticData.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count());
            return RedirectToAction(nameof(Index));

        }

        #endregion


        #region Order Summary GET
        public IActionResult OrderSummary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userID, includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userID);

            ShoppingCartVM.OrderHeader.UserName = ShoppingCartVM.OrderHeader.ApplicationUser.ApplicationUserName;
            ShoppingCartVM.OrderHeader.UserPhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.UserStreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.ApplicationUserStreetAddress;
            ShoppingCartVM.OrderHeader.UserCity = ShoppingCartVM.OrderHeader.ApplicationUser.ApplicationUserCity;
            ShoppingCartVM.OrderHeader.UserState = ShoppingCartVM.OrderHeader.ApplicationUser.ApplicationUserState;
            ShoppingCartVM.OrderHeader.UserPostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.ApplicationUserPostalCode;

            foreach (var Cart in ShoppingCartVM.ShoppingCartList)
            {
                Cart.TotalPrice = GetPriceBasedOnQuantity(Cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (Cart.TotalPrice * Cart.ProductCount);
            }
            return View(ShoppingCartVM);
        }
        #endregion

        #region Order Summary Post
        [HttpPost]
        [ActionName("OrderSummary")]
        public IActionResult OrderSummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product");
            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
            //ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            ApplicationUserModel applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);


            foreach (var Cart in ShoppingCartVM.ShoppingCartList)
            {
                Cart.TotalPrice = GetPriceBasedOnQuantity(Cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (Cart.TotalPrice * Cart.ProductCount);
            }

            if (applicationUser.CompanyID.GetValueOrDefault() == 0)
            {
                //It is regular customer account
                ShoppingCartVM.OrderHeader.PaymentStatus = StaticData.PaymentStatusPending;
                ShoppingCartVM.OrderHeader.OrderStatus = StaticData.StatusPending;

            }
            else
            {
                //it is company user
                ShoppingCartVM.OrderHeader.PaymentStatus = StaticData.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = StaticData.StatusApproved;
            }
            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetailModel orderDetail = new()
                {
                    ProductID = cart.ProductID,
                    OrderHeaderID = ShoppingCartVM.OrderHeader.OrderHeaderID,
                    OrderPrice = cart.TotalPrice,
                    ProductCount = cart.ProductCount,
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

             #region Stripe Payment Logic
            if (applicationUser.CompanyID.GetValueOrDefault() == 0)
            {
                //It is regular customer account and we need to capture Payment
                //stripe logic

                var domain = "https://localhost:7198/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"Customer/ShoppingCart/OrderConfirmation?orderid={ShoppingCartVM.OrderHeader.OrderHeaderID}",
                    CancelUrl = domain + "Customer/ShoppingCart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    BillingAddressCollection = "required", // Collect the billing address
                    ShippingAddressCollection = new SessionShippingAddressCollectionOptions // Collect shipping address if needed
                    {
                        AllowedCountries = new List<string> { "IN" } // Limit to India
                    },
                    Mode = "payment",
                };


                foreach (var item in ShoppingCartVM.ShoppingCartList)
                {
                    var SessionLineItem = new SessionLineItemOptions
                    {

                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            UnitAmount = (long)(item.TotalPrice * 100),
                            Currency = "INR",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = item.Product.ProductTitle
                            }
                        },
                        Quantity = item.ProductCount
                    };
                    options.LineItems.Add(SessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.OrderHeaderID, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);

            }
            #endregion

            return RedirectToAction(nameof(OrderConfirmation), new { orderid = ShoppingCartVM.OrderHeader.OrderHeaderID });
        }
        #endregion

        #region Order Confirmation
        public IActionResult OrderConfirmation(int orderid)
        {
            OrderHeaderModel orderHeader = _unitOfWork.OrderHeader.Get(u => u.OrderHeaderID == orderid, includeProperties: "ApplicationUser");

            if (orderHeader.PaymentStatus != StaticData.PaymentStatusDelayedPayment)
            {
                //this is not for company User

                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentID(orderid, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(orderid, StaticData.StatusApproved, StaticData.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
                List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

                _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
                _unitOfWork.Save();
                HttpContext.Session.Clear();
            }
            return View(orderid);
        }
        #endregion

        #region GetPriceBasedOnQuantity
        private double GetPriceBasedOnQuantity(ShoppingCart cart)
        {
            if (cart.ProductCount <= 50)
            {
                return cart.Product.ProductPriceOneToFifty;
            }
            else if (cart.ProductCount <= 100)
            {
                return cart.Product.ProductPriceFiftyPlus;
            }
            else if (cart.ProductCount > 100)
            {
                return cart.Product.ProductPriceHundredPlus;
            }
            else
            {
                return 0;
            }
        }
        #endregion

    }
}
