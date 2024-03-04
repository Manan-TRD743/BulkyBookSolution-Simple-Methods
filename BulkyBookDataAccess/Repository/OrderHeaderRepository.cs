using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookDataAccess.Data;


namespace BulkyBookDataAccess.Repository
{
    //Implement a IOrderHeaderRepository interface and also inherit the Repository class for OrderHeader Model
    public class OrderHeaderRepository : Repository<OrderHeaderModel>, IOrderHeaderRepository
    {
        //Create a ApplicationDbContext Object
        private readonly ApplicationDbContext _DbContext;

        public OrderHeaderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //Initialize ApplicationDbContext
            _DbContext = applicationDbContext;
        }

        //Update Method For Category
        public void Update(OrderHeaderModel orderHeader)
        {
            _DbContext.Update(orderHeader);
        }

        public void UpdateStatus(int id, string OrderStatus, string? PaymentStatus = null)
        {
            var orderFromDb = _DbContext.OrderHeaders.FirstOrDefault(u => u.OrderHeaderID == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = OrderStatus;
                if(!string.IsNullOrEmpty(PaymentStatus))
                {
                    orderFromDb.PaymentStatus = PaymentStatus;
                }
            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _DbContext.OrderHeaders.FirstOrDefault(u => u.OrderHeaderID == id);
            if(!string.IsNullOrEmpty (sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if(!string.IsNullOrEmpty (paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
