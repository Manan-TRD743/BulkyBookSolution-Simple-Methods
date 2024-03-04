using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookModel
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [ValidateNever] 
        public ProductModel Product { get; set;}

        [Range(1,1000,ErrorMessage ="Please Enter Value Between 1 to 1000.")]
        public int ProductCount { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUserModel ApplicationUser { get; set; }

        [NotMapped]
        public double TotalPrice { get; set; }

    }
}
