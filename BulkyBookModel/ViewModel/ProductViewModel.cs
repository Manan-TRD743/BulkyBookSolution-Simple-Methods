using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookModel.ViewModel
{
    /// <summary>
    /// Represents a view model for managing product-related data.
    /// </summary>
    public class ProductViewModel
    {
        //Get or Set Product Details
        public ProductModel Product { get; set; }

        //Get or Set List of Categories
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
