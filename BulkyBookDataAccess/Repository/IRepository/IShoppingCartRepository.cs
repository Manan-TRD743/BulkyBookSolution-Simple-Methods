using BulkyBookModel;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : Irepository<ShoppingCart>
    {
        //Declaration Of Methos for Update Shopping Cart
        void Update(ShoppingCart shoppingCartItem);
    }
}
