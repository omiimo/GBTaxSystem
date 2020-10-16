using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxSystem.Application.Models;

namespace TaxSystem.Application.Services
{
    public interface IPurchaseService
    {
        /// <summary>
        /// Calculates net, gross and VAT amount
        /// </summary>
        /// <param name="purchaseData">Input Purchase data</param>
        /// <returns>Calculated purchase data</returns>
        Task<PurchaseData> CalculatePurchaseInfo(PurchaseData purchaseData);
    }
}
