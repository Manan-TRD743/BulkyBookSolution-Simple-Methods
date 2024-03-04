
using BulkyBookSolution.BulkyBookDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulkyBookSolution.BulkyBookDataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240212045648_SeedCategoryTable")]
    partial class SeedCategoryTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BulkyBook.Models.CategoryModel", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<int>("CategoryDisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryDisplayOrder = 1,
                            CategoryName = "Action"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryDisplayOrder = 2,
                            CategoryName = "Sci-Fi"
                        },
                        new
                        {
                            CategoryID = 3,
                            CategoryDisplayOrder = 3,
                            CategoryName = "Histroy"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
