using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookSolution.BulkyBookModel.Models
{
    public class CategoryModel
    {
        // Gets or sets the CategoryID. This property serves as the primary key and is required.
        [Key]
        public int CategoryID { get; set;}
        [Required(ErrorMessage ="Category Name is Required.")]


        //Get Or Set Category Name.
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }


       //Get Or Set Display Order and it range between 1 to 100.
        [DisplayName("Display Order"),Range(1,100,ErrorMessage = "Display Order Must be between 1-100 .")]
        public int CategoryDisplayOrder { get; set; }
    }
}
