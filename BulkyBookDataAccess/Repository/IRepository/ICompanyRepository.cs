using BulkyBookModel;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface ICompanyRepository : Irepository<CompanyModel>
    {
        //Declaration of Update Company Details Method
        void UpdateCompany(CompanyModel company);
    }
}
