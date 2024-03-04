/*using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;

namespace BulkyBook_WebAPI.Implementation
{
    public class CategoryImplementation : Services<Category>, ICategory
    {
        private ApplicationDbContext DbContext;
        public CategoryImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public void UpdateCategory(Category category)
        {
            var updateCategory =  DbContext.CategoriesDetalis.FirstOrDefault(u=>u.CategoryID.Equals(category.CategoryID));
            if (updateCategory != null)
            {
                // Update the Category properties and save changes
                updateCategory.CategoryName = category.CategoryName;
                updateCategory.CategoryDisplayOrder = category.CategoryDisplayOrder;

            }
        }
    }
}
*/