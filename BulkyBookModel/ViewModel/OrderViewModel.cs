
namespace BulkyBookModel.ViewModel
{
    public class OrderViewModel
    {
        public OrderHeaderModel orderHeader { get; set; }
        public IEnumerable<OrderDetailModel>  orderDetails{ get; set; }
    }
}
