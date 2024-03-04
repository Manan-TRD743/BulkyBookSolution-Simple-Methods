

namespace BulkyBookModel.ViewModel
{
    public class ShoppingCartViewModel
    {
       public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        
        public OrderHeaderModel OrderHeader { get; set; }
    }
}
