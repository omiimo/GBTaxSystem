using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxSystem.WebAPI.Models
{
    public class PurchaseDataModel
    {
        /// <summary>
        /// VAT Rate: Valid values (10, 13, 20)
        /// </summary>
        [Required(ErrorMessage = "VATRate is a required field")]
        [RegularExpression("^(10|13|20){1}$", ErrorMessage = "Valid VATRates are : 10, 13, 20")]
        public decimal VATRate { get; set; }
        /// <summary>
        /// Net Amount
        /// </summary>
        public decimal? NetAmount { get; set; }
        /// <summary>
        /// Gross Amount
        /// </summary>
        public decimal? GrossAmount { get; set; }
        /// <summary>
        /// VAT Amount
        /// </summary>
        public decimal? VATAmount { get; set; }
    }
}
