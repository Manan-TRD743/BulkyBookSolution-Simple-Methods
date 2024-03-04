using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookDataAccess.Data;
using BulkyBookSolution.BulkyBookModel.Models;


namespace BulkyBookDataAccess.Repository
{
    //Implement a IOrderDetailRepository interface and also inherit the Repository class for OrderDetail Model
    public class OrderDetailRepository : Repository<OrderDetailModel>, IOrderDetailRepository
    {
        //Create a ApplicationDbContext Object
        private readonly ApplicationDbContext _DbContext;

        public OrderDetailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //Initialize ApplicationDbContext
            _DbContext = applicationDbContext;
        }

        //Update Method For orderDetail
        public void Update(OrderDetailModel orderDetail)
        {
           _DbContext.Update(orderDetail);
        }
    }
}
