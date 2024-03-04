using BulkyBookSolution.BulkyBookModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface ICategoryRepository : Irepository<CategoryModel>
    {
        //Declaration Of Methos for Update Category 
        void Update(CategoryModel category);
    }
}
