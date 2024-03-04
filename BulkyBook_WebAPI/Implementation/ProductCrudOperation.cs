using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_WebAPI.Implementation
{
    public class ProductCrudOperation : IProduct
    {
        private readonly ApplicationDbContext DbSet;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        // Constructor to initialize the database context
        public ProductCrudOperation(ApplicationDbContext dbContext, IWebHostEnvironment WebHostEnvironment)
        {
            DbSet = dbContext;
            _WebHostEnvironment = WebHostEnvironment;
        }

        #region Add Product
        public async Task AddProductAsync(Product Product, IFormFile file)
        {
            try
            {
                
                if (file != null)
                {
                    // Save the file to the server
                    string WwwRootPath = _WebHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productpath = Path.Combine(WwwRootPath, @"Images\Product");
                    using (var fileStream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    Product.ProductImgUrl = filename; // Save the file name as the product URL
                }


                // Check if the category already exists
                var existingCategory = await DbSet.CategoriesDetalis.FirstOrDefaultAsync(c => c.CategoryName!.Equals(Product.Category!.CategoryName));
                if (existingCategory == null)
                {
                    // Add the new category to the database
                    DbSet.CategoriesDetalis.Add(Product.Category!); // Add the category associated with the product
                }

                // Assign the existing or new category to the product
                Product.Category = existingCategory;

                // Add the product to the database
                DbSet.Add(Product);
                
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while adding product: " + ex.Message);
            }
        }
        #endregion

        #region Delete Product
        public async Task DeleteProductAsync(Product Product)
        {
            await Task.Run(() =>
            {
                DbSet.Remove(Product);
            });
        }
        #endregion

        #region Get All Product
        public async Task<List<Product>> GetAllProductAsync()
        {
            return await DbSet.Products.Include(c=>c.Category).ToListAsync();
        }
        #endregion

        #region Get Product from Id
        public async Task<Product> GetProductAsync(int? ProductID)
        {
            var product = await DbSet.Set<Product>()
                               .Where(u => u.ProductID.Equals(ProductID))
                               .FirstOrDefaultAsync();

            // If no product is found, throw an exception
            return product ?? throw new InvalidOperationException("Product not found");
        }
        #endregion

        #region Save Product
        public async Task SaveProductAsync()
        {
            await DbSet.SaveChangesAsync();
        }
        #endregion

        #region Update Product
        public async Task UpdateProductAsync(Product product, IFormFile file)
        {
            string WwwRootPath = _WebHostEnvironment.WebRootPath;
            try
            {

                // Save the new image file to the server if provided
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(WwwRootPath, @"Images\Product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            
                // Check if the category already exists
                var existingCategory = await DbSet.CategoriesDetalis.FirstOrDefaultAsync(c => c.CategoryName!.Equals(product.Category!.CategoryName));
            if (existingCategory == null)
            {
                // Add the new category to the database
                DbSet.CategoriesDetalis.Add(product.Category!); // Add the category associated with the product
            }

            // Assign the existing or new category to the product
            product.Category = existingCategory;

            await Task.Run(() =>
            {
                DbSet.Update(product);
            });
        }
            catch(Exception ex)
            {
                
            }
        }
        #endregion

    }
}
