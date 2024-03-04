using BulkyBook_WebAPI.Model;

namespace BulkyBook_WebAPI.Services
{
    /* public interface ICompany : IServices<Company>
     {
         void UpdateCompany(Company Company);
     }*/

    public interface ICompany 
    {
        Task AddCompanyAsync(Company Company);
        Task UpdateCompanyAsync(Company Company);
        Task DeleteCompanyAsync(Company Company);
        Task <List<Company>> GetAllCompanyAsync();
        Task<Company>  GetCompanyAsync(int? CompanyID);
        Task SaveCompanyAsync();
    }
}
