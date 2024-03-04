using BulkyBookModel;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IProductRepository : Irepository<ProductModel>
    {
     // Declaration Of Update Method for Product
        void Update(ProductModel Product);
    }
}
