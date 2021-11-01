using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class CheckoutModel
    {
        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpireDate { get; set; }
        [Required]
        [RegularExpression("([0-9][0-9][0-9])", ErrorMessage = "Please enter a valid 3 digit number")]
        public string SecurityCode { get; set; }
    }
}
