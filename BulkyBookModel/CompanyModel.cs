using System.ComponentModel.DataAnnotations;

namespace BulkyBookModel
{
    public class CompanyModel
    {
        //Get or Set Company Id and it is a key
        [Key]
        public int CompanyID { get; set; }

        //Get or Set Company Name
        [Required]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        //Get or Set Company Street Address
        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        //Get or Set Company City
        [Display(Name = "City")]
        public string? City { get; set; }

        //Get or Set Company State
        [Display(Name = "State")]
        public string? State { get; set; }

        //Get or Set Company Postal Code
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        //Get or Set Company Phone Number
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
