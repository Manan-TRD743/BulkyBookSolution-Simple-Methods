using BulkyBook_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_WebAPI.Services
{
    /* public interface ICategory : IServices<Category>
     {
         void UpdateCategory(Category category);
     }*/

    public interface ICategory 
    {
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task <List<Category>> GetAllCategoryAsync();
        Task<Category>  GetCategoryAsync(int? CategoryID);
        Task SaveCategoryAsync();
    }
}
