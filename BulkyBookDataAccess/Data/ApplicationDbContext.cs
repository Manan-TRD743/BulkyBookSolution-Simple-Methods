using BulkyBookModel;
using BulkyBookSolution.BulkyBookModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookSolution.BulkyBookDataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //Create Dbset for Categories Table
        public DbSet<CategoryModel> Categories { get; set; }

        //Create Dbset for Products Table
        public DbSet<ProductModel> Products { get; set; }

        //Create Dbset for Companies Table
        public DbSet<CompanyModel> Companies { get; set; }

        //Create Dbset for ApplicationUsers Table
        public DbSet<ApplicationUserModel> ApplicationUsers { get; set; }

        //Create Dbset for ShoppingCarts Table
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        //Create Dbset for OrderHeaders Table
        public DbSet<OrderHeaderModel> OrderHeaders { get; set; }

        //Create Dbset for OrderDetails Table
        public DbSet<OrderDetailModel> OrderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seed Category Table
            //Add Data Into Categories Table
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryID = 1, CategoryName = "Action", CategoryDisplayOrder = 1 },
                new CategoryModel { CategoryID = 2, CategoryName = "Sci-Fi", CategoryDisplayOrder = 2 },
                new CategoryModel { CategoryID = 3, CategoryName = "Histroy", CategoryDisplayOrder = 3 }
                );
            #endregion

            #region Seed Product Table
            //Add Data Into Product Table
            modelBuilder.Entity<ProductModel>().HasData(
              new ProductModel
              {
                  ProductID = 1,
                  ProductTitle = "Fortune of Time",
                  ProductAuthor = "Billy Spark",
                  ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                  ProductISBN = "SWD9999001",
                  ProductListPrice = 99,
                  ProductPriceOneToFifty = 90,
                  ProductPriceFiftyPlus = 85,
                  ProductPriceHundredPlus = 80,
                  CategoryID = 1
              },
                new ProductModel
                {
                    ProductID = 2,
                    ProductTitle = "Dark Skies",
                    ProductAuthor = "Nancy Hoover",
                    ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                    ProductISBN = "CAW777777701",
                    ProductListPrice = 40,
                    ProductPriceOneToFifty = 30,
                    ProductPriceFiftyPlus = 25,
                    ProductPriceHundredPlus = 20,
                    CategoryID = 1,
                    ProductImgUrl = ""
                },
                new ProductModel
                {
                    ProductID = 3,
                    ProductTitle = "Vanish in the Sunset",
                    ProductAuthor = "Julian Button",
                    ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                    ProductISBN = "RITO5555501",
                    ProductListPrice = 55,
                    ProductPriceOneToFifty = 50,
                    ProductPriceFiftyPlus = 40,
                    ProductPriceHundredPlus = 35,
                    CategoryID = 2,
                    ProductImgUrl = ""
                },
                new ProductModel
                {
                    ProductID = 4,
                    ProductTitle = "Cotton Candy",
                    ProductAuthor = "Abby Muscles",
                    ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                    ProductISBN = "WS3333333301",
                    ProductListPrice = 70,
                    ProductPriceOneToFifty = 65,
                    ProductPriceFiftyPlus = 60,
                    ProductPriceHundredPlus = 55,
                    CategoryID = 3,
                    ProductImgUrl = ""
                },
                new ProductModel
                {
                    ProductID = 5,
                    ProductTitle = "Rock in the Ocean",
                    ProductAuthor = "Ron Parker",
                    ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                    ProductISBN = "SOTJ1111111101",
                    ProductListPrice = 30,
                    ProductPriceOneToFifty = 27,
                    ProductPriceFiftyPlus = 25,
                    ProductPriceHundredPlus = 20,
                    CategoryID = 2,
                    ProductImgUrl = ""
                },
                new ProductModel
                {
                    ProductID = 6,
                    ProductTitle = "Leaves and Wonders",
                    ProductAuthor = "Laura Phantom",
                    ProductDescription = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincProductIDunt. ",
                    ProductISBN = "FOT000000001",
                    ProductListPrice = 25,
                    ProductPriceOneToFifty = 23,
                    ProductPriceFiftyPlus = 22,
                    ProductPriceHundredPlus = 20,
                    CategoryID = 3,
                    ProductImgUrl = ""
                }
               );
            #endregion

            #region Seed Company Table
            //Add Data in Company Model
            modelBuilder.Entity<CompanyModel>().HasData(
                     new CompanyModel { CompanyID = 1, CompanyName = "ABC Corporation", StreetAddress = "123 Main St", City = "New York", State = "NY", PostalCode = "10001", PhoneNumber = "555-123-4567" },
                     new CompanyModel { CompanyID = 2, CompanyName = "XYZ Inc.", StreetAddress = "456 Elm St", City = "Los Angeles", State = "CA", PostalCode = "90001", PhoneNumber = "555-987-6543" },
                     new CompanyModel { CompanyID = 3, CompanyName = "123 Industries", StreetAddress = "789 Oak St", City = "Chicago", State = "IL", PostalCode = "60601", PhoneNumber = "555-555-5555" },
                     new CompanyModel { CompanyID = 4, CompanyName = "Smith & Co.", StreetAddress = "101 Pine St", City = "Miami", State = "FL", PostalCode = "33101", PhoneNumber = "555-321-9876" },
                     new CompanyModel { CompanyID = 5, CompanyName = "Acme Enterprises", StreetAddress = "202 Maple St", City = "San Francisco", State = "CA", PostalCode = "94101", PhoneNumber = "555-888-9999" });
            #endregion


        }
    }
}
