using System.ComponentModel.DataAnnotations;

namespace BulkyBook_WebAPI.Model
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
