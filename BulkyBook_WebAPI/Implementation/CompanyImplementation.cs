using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_WebAPI.Implementation
{
    public class CompanyImplementation : ICompany
    {
        private readonly ApplicationDbContext DbSet;

        // Constructor to initialize the database context
        public CompanyImplementation(ApplicationDbContext dbContext)
        {
            DbSet = dbContext;
        }

        #region Add Company
        // Method to asynchronously add a new Company
        public async Task AddCompanyAsync(Company Company)
        {
            await DbSet.AddAsync(Company);
        }
        #endregion

        #region Delete Company
        // Method to asynchronously delete a Company
        public async Task DeleteCompanyAsync(Company Company)
        {
            await Task.Run(() =>
            {
                DbSet.Remove(Company);
            });
        }
        #endregion

        #region Get All Company
        // Method to asynchronously get all categories
        public async Task<List<Company>> GetAllCompanyAsync()
        {
            // Return a list of all categories asynchronously
            return await DbSet.Companies.ToListAsync();
        }
        #endregion

        #region Get Company
        // Method to asynchronously get a Company by its ID
        public async Task<Company> GetCompanyAsync(int? CompanyId)
        {
            // Query the database for the Company with the given ID
            var Company = await DbSet.Set<Company>()
                               .Where(u => u.CompanyID.Equals(CompanyId))
                               .FirstOrDefaultAsync();

            // If no Company is found, throw an exception
            return Company ?? throw new InvalidOperationException("Company not found");
        }
        #endregion

        #region Save Company
        // Method to asynchronously save changes to the database
        public async Task SaveCompanyAsync()
        {
            await DbSet.SaveChangesAsync();
        }
        #endregion

        #region Update Company
        // Method to asynchronously update a Company
        public async Task UpdateCompanyAsync(Company Company)
        {
            await Task.Run(() =>
            {
                DbSet.Update(Company);
            });
        }
        #endregion
    }
}
