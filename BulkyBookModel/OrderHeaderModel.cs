using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookModel
{
    public class OrderHeaderModel
    {
        [Key]
        public int OrderHeaderID { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUserModel ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateOnly ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }


        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

         
        [Required]
        public string UserPhoneNumber { get; set; }
        [Required]
        public string UserStreetAddress { get; set; }
        [Required]
        public string UserCity { get; set; }
        [Required]
        public string UserState { get; set; }
        [Required]
        public string UserPostalCode { get; set; }
        [Required]
        public string UserName { get; set; }







    }
}
