using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_WebAPI.Implementation
{
    public class CategoryCrudOperation : ICategory
    {
        private readonly ApplicationDbContext DbSet;

        // Constructor to initialize the database context
        public CategoryCrudOperation(ApplicationDbContext dbContext)
        {
            DbSet = dbContext;
        }

        #region Add Category
        // Method to asynchronously add a new category
        public async Task AddCategoryAsync(Category category)
        {
            await DbSet.AddAsync(category);
        }
        #endregion

        #region Delete Category
        // Method to asynchronously delete a category
        public async Task DeleteCategoryAsync(Category category)
        {
            await Task.Run(() =>
            {
                DbSet.Remove(category);
            });
        }
        #endregion

        #region Get All Category
        // Method to asynchronously get all categories
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            // Return a list of all categories asynchronously
            return await DbSet.CategoriesDetalis.ToListAsync();
        }
        #endregion

        #region Get Category
        // Method to asynchronously get a category by its ID
        public async Task<Category> GetCategoryAsync(int? categoryId)
        {
            // Query the database for the category with the given ID
            var category = await DbSet.Set<Category>()
                               .Where(u => u.CategoryID.Equals(categoryId))
                               .FirstOrDefaultAsync();

            // If no category is found, throw an exception
            return category ?? throw new InvalidOperationException("Category not found");
        }
        #endregion

        #region Save Category
        // Method to asynchronously save changes to the database
        public async Task SaveCategoryAsync()
        {
            await DbSet.SaveChangesAsync();
        }
        #endregion

        #region Update Category
        // Method to asynchronously update a category
        public async Task UpdateCategoryAsync(Category category)
        {
            await Task.Run(() =>
            {
                DbSet.Update(category);
            });
        }
        #endregion

    }
}
