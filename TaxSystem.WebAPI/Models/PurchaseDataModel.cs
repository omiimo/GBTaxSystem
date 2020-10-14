using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxSystem.WebAPI.Models
{
    public class PurchaseDataModel
    {
        [Required(ErrorMessage = "VATRate is a required field")]
        [RegularExpression("^(10|13|20){1}$", ErrorMessage = "Valid VATRates are : 10, 13, 20")]
        public decimal VATRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? VATAmount { get; set; }
    }
}
