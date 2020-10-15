using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxSystem.Application.Exceptions;
using TaxSystem.Application.Models;

namespace TaxSystem.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        public Task<PurchaseData> CalculatePurchaseInfo(PurchaseData purchaseData)
        {
            ValidateRequest(purchaseData);

            var vatrate = purchaseData.VATRate / 100M;
            if (purchaseData.VATAmount != 0 && purchaseData.VATAmount != null)
            {
                purchaseData.GrossAmount = Math.Round((purchaseData.VATAmount.Value * (1 + vatrate) / vatrate), 2);
                purchaseData.NetAmount = Math.Round((purchaseData.VATAmount.Value / vatrate), 2);
                return Task.FromResult(purchaseData);
            }

            if (purchaseData.GrossAmount != 0 && purchaseData.GrossAmount != null)
            {
                purchaseData.VATAmount = Math.Round(purchaseData.GrossAmount.Value * (vatrate) / (1 + vatrate), 2);
                purchaseData.NetAmount = Math.Round(purchaseData.GrossAmount.Value / (1 + vatrate), 2);
                return Task.FromResult(purchaseData);
            }
            if (purchaseData.NetAmount != 0 && purchaseData.NetAmount != null)
            {
                purchaseData.GrossAmount = Math.Round((purchaseData.NetAmount.Value * (1 + vatrate)), 2);
                purchaseData.VATAmount = Math.Round((purchaseData.NetAmount.Value * vatrate), 2);
                return Task.FromResult(purchaseData);
            }
            return Task.FromResult(purchaseData);
        }


        private void ValidateRequest(PurchaseData purchaseData)
        {
            IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

            //Valid VAT rate for Austria - 10, 13, 20
            if (purchaseData.VATRate != 10 && purchaseData.VATRate != 13 && purchaseData.VATRate != 20)
            {
                errors.Add(nameof(purchaseData.VATRate), new string[] { "Mandatory Field. Valid VAT rates for Austria are 10, 13, 20" });
            }

            if (purchaseData.GrossAmount == null && purchaseData.NetAmount == null && purchaseData.VATAmount == null)
            {
                errors.Add(nameof(PurchaseData),
                    new string[] { "There should be at least one Input" });
            }

            if (purchaseData.GrossAmount == 0 || purchaseData.NetAmount == 0 || purchaseData.VATAmount == 0)
            {
                errors.Add(nameof(PurchaseData),
                    new string[] { "Input values should not be 0" });
            }

            if (purchaseData.GrossAmount > 0 && (purchaseData.NetAmount != null || purchaseData.VATAmount != null))
            {
                errors.Add(nameof(purchaseData.GrossAmount),
                    new string[] { "Only one input is allowed" });
            }

            if (purchaseData.NetAmount > 0 && (purchaseData.GrossAmount != null || purchaseData.VATAmount != null))
            {
                errors.Add(nameof(purchaseData.NetAmount),
                    new string[] { "Only one input is allowed" });
            }

            if (purchaseData.VATAmount > 0 && (purchaseData.NetAmount != null || purchaseData.GrossAmount != null))
            {
                errors.Add(nameof(purchaseData.VATAmount),
                   new string[] { "Only one input is allowed" });
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }
    }
}
