using BulkyBookSolution.BulkyBookModel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook_WebAPI.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }= string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductISBN { get; set; } = string.Empty;
        public string ProductAuthor { get; set; } = string.Empty;
        public double ProductListPrice { get; set; }
        public float ProductPriceOneToFifty { get; set; }
        public float ProductPriceFiftyPlus { get; set; }
        public float ProductPriceHundredPlus { get; set; }
        [ForeignKey(nameof(ProductID))]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
        public string ProductImgUrl { get; set; } = string.Empty;

    }
}
