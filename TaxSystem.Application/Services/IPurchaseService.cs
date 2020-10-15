using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxSystem.Application.Models;

namespace TaxSystem.Application.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseData> CalculatePurchaseInfo(PurchaseData purchaseData);
    }
}
