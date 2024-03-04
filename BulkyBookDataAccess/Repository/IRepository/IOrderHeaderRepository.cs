using BulkyBookModel;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : Irepository<OrderHeaderModel>
    {
        //Declaration Of Methos for Update orderHeader 
        void Update(OrderHeaderModel orderHeader);

        void UpdateStatus(int id, string OrderStatus, string? PaymentStatus = null);

        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
    }
}
