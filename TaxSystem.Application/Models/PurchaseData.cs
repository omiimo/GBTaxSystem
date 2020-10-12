using System;
using System.Collections.Generic;
using System.Text;

namespace TaxSystem.Application.Models
{
    public class PurchaseData
    {
        public decimal VATRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? VATAmount { get; set; }
    }
}
