using BulkyBook_WebAPI.Implementation;
using BulkyBook_WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        // DbSet representing the Category table in the database
       public DbSet<Category> CategoriesDetalis { get; set; }

       public DbSet<Product> Products {  get; set; }

       public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(CategoryEntity =>
            {
                // Configuring primary key
                CategoryEntity.HasKey(e => e.CategoryID);
                CategoryEntity.ToTable("Categories");

                // Configuring properties for the Category entity
                CategoryEntity.Property(e => e.CategoryName).HasMaxLength(int.MaxValue);
                CategoryEntity.Property(e=> e.CategoryDisplayOrder).HasColumnName("CategoryDisplayOrder");

            });


            #region Model Builder for Product
            modelBuilder.Entity<Product>(productEntity =>
            {
                // Configuring primary key
                productEntity.HasKey(e => e.ProductID);
                productEntity.ToTable("Products");

                // Configure properties for the Product entity
                productEntity.Property(e => e.ProductTitle).HasMaxLength(int.MaxValue);
                productEntity.Property(e => e.ProductDescription).HasMaxLength(int.MaxValue);
                productEntity.Property(e => e.ProductISBN).HasMaxLength(50); // Adjust length as needed
                productEntity.Property(e => e.ProductAuthor).HasMaxLength(int.MaxValue);
                productEntity.Property(e => e.ProductImgUrl).HasMaxLength(int.MaxValue);


                productEntity.HasOne(e => e.Category)
                     .WithMany()
                     .HasForeignKey(e => e.CategoryID)
                     .HasConstraintName("FK_Products_Categories");


                productEntity.Property(e => e.ProductListPrice).HasColumnType("float");
                productEntity.Property(e => e.ProductPriceOneToFifty).HasColumnType("float");
                productEntity.Property(e => e.ProductPriceFiftyPlus).HasColumnType("float");
                productEntity.Property(e => e.ProductPriceHundredPlus).HasColumnType("float");

            });

            #endregion

            #region Model Builder for Company
            // Configure Company entity
            modelBuilder.Entity<Company>(entity =>
            {
                // Set CompanyID as primary key
                entity.HasKey(e => e.CompanyID);

                // Configure properties
                entity.Property(e => e.CompanyName)
                    .IsRequired();

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(int.MaxValue); 

                entity.Property(e => e.City)
                    .HasMaxLength(int.MaxValue);

                entity.Property(e => e.State)
                    .HasMaxLength(int.MaxValue); 

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(int.MaxValue); 

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(int.MaxValue); 
            });
            #endregion
        }

        public static implicit operator ApplicationDbContext(CategoryCrudOperation v)
        {
            throw new NotImplementedException();
        }
    }
}
