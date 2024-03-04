using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProduct ProductObj;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public ProductController(IProduct product,IWebHostEnvironment webHostEnvironment)
        {
            ProductObj = product;
            WebHostEnvironment = webHostEnvironment;
        }
        #region GetData
        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                // Retrieve all Product from the database
                var GetProduct = await ProductObj.GetAllProductAsync();
                if (GetProduct == null)
                {
                    // Handle the case where Product is null
                    return StatusCode(500, "Failed to retrieve Product from the database.");
                }
                else
                {
                    // Return success response with the list of Product
                    return Ok(new { StatusCode = 200, status = "Success", Products = GetProduct });
                }

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }


        // GET: api/Product/5
        [HttpPost("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            try
            {
                if (id < 1)
                {
                    // Bad request if id is invalid
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                // Retrieve a specific Product by id
                var Product = await ProductObj.GetProductAsync(id);
               if (Product == null)
                {
                    // Return NotFound if the Product is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                // Return the Product if found
                return Ok(new { StatusCode = 200, status = "Success", Products = Product });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }

        }
        #endregion

        /*#region Add Product
        // POST: api/Products/
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound(new { StatusCode = 404, Status = "Product not found" });
                }
                await ProductObj.AddProductAsync(product);
                await ProductObj.SaveProductAsync();

                return Ok(new { StatusCode = 200, status = "Success", Products = product });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion*/

        #region Add Product
        // POST: api/Products/upload
        [HttpPost("upload")]
        public async Task<IActionResult> PostProduct([FromForm] Product product, IFormFile file)
        {
            try
            {
                if (product == null)
                {
                    return NotFound(new { StatusCode = 404, Status = "Product not found" });
                }

                await ProductObj.AddProductAsync(product,file);
                await ProductObj.SaveProductAsync();

                return Ok(new { StatusCode = 200, status = "Success", Products = product });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        #region Delete Product
        // DELETE: api/Product/5
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
                // Find the Product to delete by id
                Product DeleteProduct = await ProductObj.GetProductAsync(id);

                if (DeleteProduct == null)
                {
                    // Return NotFound if the Product to delete is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                else
                {
                    await ProductObj.DeleteProductAsync(DeleteProduct);
                    await ProductObj.SaveProductAsync();
                    return Ok(new { StatusCode = 200, status = "Success" });

                }


                // Remove the Product and save changes
                //UnitOfWorkObj.Product.Remove(DeleteProduct); 
                //UnitOfWorkObj.Save();


            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        /*        #region Update Product
                // PUT: api/Products
                [HttpPut]
                public async Task<IActionResult> UpdateProduct(Product ProductData)
                {
                    try
                    {
                        if (ProductData == null || ProductData.ProductID == 0)
                        {
                            // Bad request if ProductData is null or has an invalid ProductId
                            return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                        }
                        //Update Product
                        // UnitOfWorkObj.Product.UpdateProduct(ProductData);
                        await ProductObj.UpdateProductAsync(ProductData);
                        await ProductObj.SaveProductAsync();
                        return Ok(new { StatusCode = 200, status = "Success", Products = ProductData });

                    }
                    catch (Exception ex)
                    {
                        // Return error response with 500 status code
                        return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
                    }
                }
                #endregion*/

        #region Update Product
        // PUT: api/Products/upload/{id}
        [HttpPut("update")]
        public async Task<IActionResult> PutProduct(int id,[FromForm] Product product, IFormFile file)
        {
            try {
                string WwwRootPath = WebHostEnvironment.WebRootPath;
                // Retrieve the existing product from the database
                var existingProduct = await ProductObj.GetProductAsync(id);
                if (existingProduct == null)
                {
                return NotFound(new { StatusCode = 404, Status = "Product not found" });
                }

                // Delete the old image file if it exists
                if (!string.IsNullOrEmpty(existingProduct.ProductImgUrl))
                {
                    string productPath = Path.Combine(WwwRootPath, @"Images\Product");
                    string oldImagePath = Path.Combine(productPath, existingProduct.ProductImgUrl);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                // Save changes to the database
                await ProductObj.UpdateProductAsync(product,file);
                await ProductObj.SaveProductAsync();

                return Ok(new { StatusCode = 200, status = "Success", Products = existingProduct });
            }
            catch (Exception ex)
            {
               
                // Return error response with 500 status code and log sensitive data
                return StatusCode(500, new { error = "Internal Server Error: " + ex.Message });
            }
        }
        #endregion


    }
}
