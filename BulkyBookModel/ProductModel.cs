using BulkyBookSolution.BulkyBookModel.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookModel
{
    public class ProductModel
    {
        //Get or Set ProductId And it is key
        [Key]
        public int ProductID { get; set; }

        //Get or Set Product Title And it is Required
        [Required]
        [Display(Name = "Product Title")]
        public string ProductTitle { get; set; }

        //Get or Set Product Description And it is Required
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        //Get or Set Product ISBN And it is Required
        [Required]
        [Display(Name = "Product ISBN")]
        public string ProductISBN { get; set; }

        //Get or Set Product Author And it is Required
        [Required]
        [Display(Name = "Product Author")]
        public string ProductAuthor { get; set; }

        //Get or Set Product List Price And it is Required
        [Required]
        [Display(Name ="List Price")]
        [Range(1,1000)]
        public double ProductListPrice { get; set; }

        //Get or Set Product Price for 1 to 50 Product And it is Required
        [Required]
        [Display(Name = "Product Price for 1 to 50")]
        [Range(1, 1000)]
        public double ProductPriceOneToFifty { get; set; }

        //Get or Set Product Price for 50+ Product And it is Required
        [Required]
        [Display(Name = "Product Price for 50+")]
        [Range(1, 1000)]
        public double ProductPriceFiftyPlus { get; set; }

        //Get or Set Product Price for 100+ Product And it is Required
        [Required]
        [Display(Name = "Product Price for 100+")]
        [Range(1, 1000)]
        public double ProductPriceHundredPlus { get; set; }

        //Get or Set CategoryId and it is a ForeignKey
        public int CategoryID { get; set;}
        [ForeignKey("CategoryID")]
        [ValidateNever]

        //Get or Set Category Model
        public CategoryModel Category { get; set; }

        //Get or Set Product Image and it is not Required
        [ValidateNever]
        [Display(Name ="Select Product Image")]
        public string? ProductImgUrl { get; set; }

}
}
