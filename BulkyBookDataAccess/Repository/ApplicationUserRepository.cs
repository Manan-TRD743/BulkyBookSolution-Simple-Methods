using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookDataAccess.Data;
using BulkyBookSolution.BulkyBookModel.Models;


namespace BulkyBookDataAccess.Repository
{
    //Implement a IApplicationUserRepository interface and also inherit the Repository class for Category Model
    public class ApplicationUserRepository : Repository<ApplicationUserModel>, IApplicationUserRepository
    {
        //Create a ApplicationDbContext Object
        private readonly ApplicationDbContext CategoryDbContext;

        public ApplicationUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //Initialize ApplicationDbContext
            CategoryDbContext = applicationDbContext;
        }

    }
}
