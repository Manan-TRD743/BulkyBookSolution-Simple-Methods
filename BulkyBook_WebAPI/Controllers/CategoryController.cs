using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Implementation;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        /*private readonly IUnitOfWork UnitOfWorkObj;
                public CategoryController(IUnitOfWork unitOfWork)
                {
                    UnitOfWorkObj = unitOfWork;
         }*/
        private readonly ICategory CategoryObj;
        public CategoryController(ICategory category)
        {
            CategoryObj = category;
        }

        #region GetData
        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                // Retrieve all Category from the database
                // var GetCategory = UnitOfWorkObj.Category.GetAll();

                var GetCategory = await CategoryObj.GetAllCategoryAsync();
                if (GetCategory == null)
                {
                    // Handle the case where Category is null
                    return StatusCode(500, "Failed to retrieve Category from the database.");
                }
                else
                {
                    // Return success response with the list of Category
                    return Ok(new { StatusCode = 200, status = "Success", Categories = GetCategory });
                }

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }

        // GET: api/Category/5
        [HttpPost("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {

            try
            {
                if (id < 1)
                {
                    // Bad request if id is invalid
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                // Retrieve a specific Category by id
                // var category = UnitOfWorkObj.Category.Get(u => u.CategoryID.Equals(id));


                var category =await CategoryObj.GetCategoryAsync(id);
                if (category == null)
                {
                    // Return NotFound if the Category is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                // Return the Category if found
                return Ok(new { StatusCode = 200, status = "Success", categories = category });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }

        }
        #endregion

        #region Add Data
        // POST: api/Categorys
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category Category)
        {
            try
            {
                if (Category == null)
                {
                    return NotFound(new { StatusCode = 404, Status = "Category not found" });
                }
                await CategoryObj.AddCategoryAsync(Category);
               await CategoryObj.SaveCategoryAsync();

                // UnitOfWorkObj.Category.Add(Category);
                // UnitOfWorkObj.Save();
                return Ok(new { StatusCode = 200, status = "Success", Categorys = Category });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        #region Update Data
        // PUT: api/Categorys
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category CategoryData)
        {
            try
            {
                if (CategoryData == null || CategoryData.CategoryID == 0)
                {
                    // Bad request if CategoryData is null or has an invalid CategoryId
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                //Update Category
                // UnitOfWorkObj.Category.UpdateCategory(CategoryData);
                await CategoryObj.UpdateCategoryAsync(CategoryData);
                await CategoryObj.SaveCategoryAsync();
                return Ok(new { StatusCode = 200, status = "Success", Categorys = CategoryData });

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        #region Delete Data
        // DELETE: api/Categorys/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id < 1)
                {
                    // Bad request if id is invalid
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                // Find the Category to delete by id
                Category DeleteCategory = await CategoryObj.GetCategoryAsync(id);

                if (DeleteCategory == null)
                {
                    // Return NotFound if the Category to delete is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                else
                {
                   await CategoryObj.DeleteCategoryAsync(DeleteCategory);
                    await CategoryObj.SaveCategoryAsync();
                    return Ok(new { StatusCode = 200, status = "Success" });

                }


                // Remove the Category and save changes
                //UnitOfWorkObj.Category.Remove(DeleteCategory); 
                //UnitOfWorkObj.Save();


            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion
    }
        
    }

