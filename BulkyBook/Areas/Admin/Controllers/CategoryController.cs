using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookSolution.BulkyBookModel.Models;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    // Authorize access only for users with "Admin" role
    [Authorize(Roles =StaticData.RoleUserAdmin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork CategoryUnitOfWork;

        public CategoryController(IUnitOfWork UnitOfWorkCategory)
        {
            CategoryUnitOfWork = UnitOfWorkCategory;
        }

        #region Display Category
        //Display All category 
        public IActionResult Index()
        {
            List<CategoryModel> objCategoryList = CategoryUnitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        #endregion

        #region CreateNewCategory
        //Create New Category
        public IActionResult CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewCategory(CategoryModel obj)
        {
            //Category name and category display order are not the same.
            if (obj.CategoryName == obj.CategoryDisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Category name and Display order are not the same.");

            }

            if (ModelState.IsValid)
            {
                //Add Category
                CategoryUnitOfWork.Category.Add(obj);
                CategoryUnitOfWork.Save();
                TempData["Success"] = "New Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        #endregion

        #region Edit Category
        //Edit Category
        public IActionResult EditCategory(int? id)
        {
            //if id is null or zero then return notfound
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Find Category Item using Id
            CategoryModel? CategoryFromDb = CategoryUnitOfWork.Category.Get(u => u.CategoryID == id);
            //if CategoryFromDb is null or zero then return notfound
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel obj)
        {
            if (ModelState.IsValid)
            {
                CategoryUnitOfWork.Category.Update(obj);
                CategoryUnitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        #endregion

        #region Delete Category

        //Delete Category 
        public IActionResult DeleteCategory(int? id)
        {
            //check id is null or zero
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Get category from id
            CategoryModel? CategoryFromDb = CategoryUnitOfWork.Category.Get(u => u.CategoryID.Equals(id));
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
         
        [HttpPost, ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryPost(CategoryModel obj)
        {
            CategoryUnitOfWork.Category.Remove(obj);
            CategoryUnitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
        #endregion

        
    }
}
