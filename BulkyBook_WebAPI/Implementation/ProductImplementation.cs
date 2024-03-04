/*using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_WebAPI.Implementation
{
    public class ProductImplementation : Services<Product>, IProduct
    {
        private ApplicationDbContext DbContext;
        public ProductImplementation(ApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }

        public void UpdateProduct(Product product)
        {
           var UpdateProduct = DbContext.Products.FirstOrDefault(u=>u.ProductID == product.ProductID);
            if(UpdateProduct != null)
            {
                UpdateProduct.ProductTitle = product.ProductTitle;
                UpdateProduct.ProductDescription = product.ProductDescription;
                UpdateProduct.ProductISBN = product.ProductISBN;
                UpdateProduct.ProductAuthor = product.ProductAuthor;
                UpdateProduct.ProductListPrice = product.ProductListPrice;
                UpdateProduct.ProductPriceOneToFifty = product.ProductPriceOneToFifty;
                UpdateProduct.ProductPriceFiftyPlus = product.ProductPriceFiftyPlus;
                UpdateProduct.ProductPriceHundredPlus = product.ProductPriceHundredPlus;
                UpdateProduct.Category = product.Category;
                UpdateProduct.ProductImgUrl = product.ProductImgUrl;
            }
        }
    }
}
*/