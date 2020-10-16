using System;
using System.Collections.Generic;
using System.Text;

namespace TaxSystem.Application.Models
{
    public class PurchaseData
    {
        /// <summary>
        /// VAT Rate: Valid values (10,13,20) for Austria
        /// </summary>
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
