using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookSolution.BulkyBookDataAccess.Data;

namespace BulkyBookDataAccess.Repository
{
    //Implement a IProductRepository interface and also inherit the Repository class for Product Model
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        //Create a ApplicationDbContext Object
        private readonly ApplicationDbContext ProductDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            //Initialize ApplicationDbContext
            ProductDbContext = applicationDbContext;
        }

        //Update Method For Product
        public void Update(ProductModel Productmodel)
        {
            //Get a Product from ProductID For update Company Details
            var productFromDb =  ProductDbContext.Products.FirstOrDefault(u=>u.ProductID.Equals(Productmodel.ProductID));

            //Check Product is null or not
            if(productFromDb != null)
            {
                //Update the Product Details
                productFromDb.ProductTitle = Productmodel.ProductTitle;
                productFromDb.ProductISBN = Productmodel.ProductISBN;
                productFromDb.ProductListPrice = Productmodel.ProductListPrice;
                productFromDb.ProductPriceFiftyPlus = Productmodel.ProductPriceFiftyPlus;
                productFromDb.ProductPriceHundredPlus = Productmodel.ProductPriceHundredPlus;
                productFromDb.ProductDescription = Productmodel.ProductDescription;
                productFromDb.CategoryID = Productmodel.CategoryID;
                productFromDb.ProductAuthor = Productmodel.ProductAuthor;
                if (Productmodel.ProductImgUrl != null)
                {
                    productFromDb.ProductImgUrl = Productmodel.ProductImgUrl;
                }
            }
        }
    }
}
