using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookDataAccess.Data;
using BulkyBookSolution.BulkyBookModel.Models;


namespace BulkyBookDataAccess.Repository
{
    //Implement a IShoppingCartRepository interface and also inherit the Repository class for Category Model
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        //Create a ApplicationDbContext Object
        private readonly ApplicationDbContext CartDbContext;

        public ShoppingCartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //Initialize ApplicationDbContext
            CartDbContext = applicationDbContext;
        }

        //Update Method For Category
        public void Update(ShoppingCart cart)
        {
            CartDbContext.Update(cart);
        }
    }
}
