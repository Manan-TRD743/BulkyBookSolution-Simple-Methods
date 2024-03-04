using BulkyBookDataAccess.Repository;
using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookModel.ViewModel;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        [BindProperty]
        public OrderViewModel objOrderVM { get; set; }


        public OrderController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderDetails(int OrderID)
        {
            objOrderVM = new()
            {
                orderHeader = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == OrderID, includeProperties: "ApplicationUser"),
                orderDetails = _UnitOfWork.OrderDetail.GetAll(u => u.OrderHeaderID == OrderID, includeProperties: "Product")
            };
            return View(objOrderVM);
        }

        #region UpdateOrderDetails  
        [HttpPost]
        [Authorize(Roles =StaticData.RoleUserAdmin+","+StaticData.RoleUserEmployee)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == objOrderVM.orderHeader.OrderHeaderID);

            orderHeaderFromDb.UserName = objOrderVM.orderHeader.UserName;
            orderHeaderFromDb.UserPhoneNumber = objOrderVM.orderHeader.UserPhoneNumber;
            orderHeaderFromDb.UserStreetAddress = objOrderVM.orderHeader.UserStreetAddress;
            orderHeaderFromDb.UserCity = objOrderVM.orderHeader.UserCity;
            orderHeaderFromDb.UserState = objOrderVM.orderHeader.UserState;
            orderHeaderFromDb.UserPostalCode = objOrderVM.orderHeader.UserPostalCode;

            if (!string.IsNullOrEmpty(objOrderVM.orderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = objOrderVM.orderHeader.Carrier;

            }
            if (!string.IsNullOrEmpty(objOrderVM.orderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = objOrderVM.orderHeader.TrackingNumber;
            }
            _UnitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _UnitOfWork.Save();

            TempData["success"] = "Order Details Updated Successfully";
            return RedirectToAction(nameof(OrderDetails), new { OrderID = orderHeaderFromDb.OrderHeaderID});
        }

        #endregion
    
        #region GetAll Order (Api Call)
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderModel> objorderHeaders = _UnitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            if(User.IsInRole(StaticData.RoleUserAdmin) || User.IsInRole(StaticData.RoleUserEmployee))
            {
                objorderHeaders = _UnitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                objorderHeaders = _UnitOfWork.OrderHeader.GetAll(u=>u.ApplicationUserId==userID,includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "inprocess":
                    objorderHeaders = objorderHeaders.Where(u => u.OrderStatus == StaticData.StatusInProcess);
                    break;
                case "pending":
                    objorderHeaders = objorderHeaders.Where(u => u.PaymentStatus == StaticData.PaymentStatusDelayedPayment);
                    break;
                case "completed":
                    objorderHeaders = objorderHeaders.Where(u => u.OrderStatus == StaticData.StatusShipped);
                    break;

                case "approved":
                    objorderHeaders = objorderHeaders.Where(u => u.OrderStatus == StaticData.StatusApproved );
                    break;

                default:
                    break;

            }

            return Json(new { Data = objorderHeaders });
        }
        #endregion

        #region Order Processing
        [HttpPost]
        [Authorize(Roles = StaticData.RoleUserAdmin + "," + StaticData.RoleUserEmployee)]
        public IActionResult StartOrderProcessing()
        {
            _UnitOfWork.OrderHeader.UpdateStatus(objOrderVM.orderHeader.OrderHeaderID, StaticData.StatusInProcess);
            _UnitOfWork.Save();
            TempData["success"] = "Now OrderStatus is in Processing";
            return RedirectToAction(nameof(OrderDetails), new { OrderID = objOrderVM.orderHeader.OrderHeaderID });
        }
        #endregion

        #region Order Shipping
        [HttpPost]
        [Authorize(Roles = StaticData.RoleUserAdmin + "," + StaticData.RoleUserEmployee)]
        public IActionResult OrderShipping()
        {
            var orderheader = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == objOrderVM.orderHeader.OrderHeaderID);
            orderheader.TrackingNumber = objOrderVM.orderHeader.TrackingNumber;
            orderheader.Carrier = objOrderVM.orderHeader.Carrier;
            orderheader.OrderStatus =StaticData.StatusShipped;
            orderheader.ShippingDate = DateOnly.FromDateTime(DateTime.Now);
            if(orderheader.PaymentStatus == StaticData.PaymentStatusDelayedPayment)
            {
                orderheader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }
            _UnitOfWork.OrderHeader.Update(orderheader);
            _UnitOfWork.Save();

            TempData["success"] = "Order Shipped Successfully.";
            return RedirectToAction(nameof(OrderDetails), new { OrderID = objOrderVM.orderHeader.OrderHeaderID });
        }
        #endregion

        #region Cancel Order
        [HttpPost]
        [Authorize(Roles = StaticData.RoleUserAdmin + "," + StaticData.RoleUserEmployee)]
        public IActionResult CancelOrder()
        {
            var orderheader = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == objOrderVM.orderHeader.OrderHeaderID);

            if (orderheader.PaymentStatus == StaticData.PaymentStatusApproved)
            {
                var options = new RefundCreateOptions()
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderheader.PaymentIntentId
                };
                var service = new RefundService();
                Refund refund = service.Create(options);

                _UnitOfWork.OrderHeader.UpdateStatus(orderheader.OrderHeaderID, StaticData.StatusCancelled, StaticData.StatusRefunded);
            }
            else
            {
                _UnitOfWork.OrderHeader.UpdateStatus(orderheader.OrderHeaderID, StaticData.StatusCancelled, StaticData.StatusCancelled);
            }
            _UnitOfWork.Save();
            TempData["success"] = "Order Cancelled Successfully.";
            return RedirectToAction(nameof(OrderDetails), new { OrderID = objOrderVM.orderHeader.OrderHeaderID });
        }
        #endregion

        #region Pay Delay Payment
        [HttpPost]
        public IActionResult PayDelayPayment()
        {
            objOrderVM.orderHeader = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == objOrderVM.orderHeader.OrderHeaderID,includeProperties:"ApplicationUser");
            objOrderVM.orderDetails = _UnitOfWork.OrderDetail.GetAll(u => u.OrderHeaderID == objOrderVM.orderHeader.OrderHeaderID, includeProperties:"Product");

            //stripe logic

            var domain = "https://localhost:7198/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"admin/Order/PaymentConfirmation?orderHeaderId={objOrderVM.orderHeader.OrderHeaderID}",
                CancelUrl = domain + $"admin/Order/OrderDetails?OrderID={objOrderVM.orderHeader.OrderHeaderID}",
                LineItems = new List<SessionLineItemOptions>(),
                BillingAddressCollection = "required", // Collect the billing address
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions // Collect shipping address if needed
                {
                    AllowedCountries = new List<string> { "IN" } // Limit to India
                },
                Mode = "payment",
            };


            foreach (var item in objOrderVM.orderDetails)
            {
                var SessionLineItem = new SessionLineItemOptions
                {

                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)(item.OrderPrice * 100),
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
            _UnitOfWork.OrderHeader.UpdateStripePaymentID(objOrderVM.orderHeader.OrderHeaderID, session.Id, session.PaymentIntentId);
            _UnitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        #endregion

        #region Payment Confirmation
       
        public IActionResult PaymentConfirmation(int orderHeaderId)
        {
            OrderHeaderModel orderHeader = _UnitOfWork.OrderHeader.Get(u => u.OrderHeaderID == orderHeaderId);

            if (orderHeader.PaymentStatus == StaticData.PaymentStatusDelayedPayment)
            {
                //this is an order by company User

                var service = new SessionService(); 
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _UnitOfWork.OrderHeader.UpdateStripePaymentID(orderHeaderId, session.Id, session.PaymentIntentId);
                    _UnitOfWork.OrderHeader.UpdateStatus(orderHeaderId, orderHeader.OrderStatus, StaticData.PaymentStatusApproved);
                    _UnitOfWork.Save();
                }
                List<ShoppingCart> shoppingCarts = _UnitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

                _UnitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
                _UnitOfWork.Save();

            }
            return View(orderHeaderId);
        }
        #endregion
    }
}
