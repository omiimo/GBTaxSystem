using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using TaxSystem.Application.Exceptions;
using TaxSystem.Application.Models;

namespace TaxSystem.Application.PurchaseInfo.Commands
{
    public class CalculatePurchaseCommand : IRequest<PurchaseData>
    {
        public decimal VATRate { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VATAmount { get; set; }

        public class Handler : IRequestHandler<CalculatePurchaseCommand, PurchaseData>
        {
            public Task<PurchaseData> Handle(CalculatePurchaseCommand request, CancellationToken cancellationToken)
            {
                ValidateRequest(request);

                PurchaseData purchaseData = new PurchaseData
                {
                    GrossAmount = request.GrossAmount,
                    NetAmount = request.NetAmount,
                    VATAmount = request.VATAmount,
                    VATRate =  request.VATRate
                };

                var vatrate = purchaseData.VATRate / 100;
                if (purchaseData.VATAmount != 0)
                {
                    purchaseData.NetAmount = Math.Round((purchaseData.VATAmount * (1 + vatrate)/vatrate), 2 );
                    purchaseData.GrossAmount = Math.Round((purchaseData.VATAmount / vatrate), 2);
                    return Task.FromResult(purchaseData);
                }

                if (purchaseData.NetAmount != 0)
                {
                    purchaseData.VATAmount = Math.Round(purchaseData.NetAmount * (vatrate) / (1 + vatrate), 2);
                    purchaseData.GrossAmount = Math.Round(purchaseData.NetAmount / (1 + vatrate), 2);
                    return Task.FromResult(purchaseData);
                }
                if (purchaseData.NetAmount != 0)
                {
                    purchaseData.NetAmount = Math.Round((purchaseData.GrossAmount * (1 + vatrate)), 2);
                    purchaseData.VATAmount = Math.Round((purchaseData.GrossAmount * vatrate), 2);
                    return Task.FromResult(purchaseData);
                }
                return Task.FromResult(purchaseData);
            }

            private void ValidateRequest(CalculatePurchaseCommand request)
            {
                IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

                if (request.VATRate != 10 && request.VATRate != 13 && request.VATRate != 20)
                {
                    errors.Add(nameof(request.VATRate), new string[] {"Valid VAT Rates for Austria are 10, 13, 20"});
                }

                if(request.VATRate != 0 && request.GrossAmount <= 0 && request.NetAmount <= 0 && request.VATAmount <=0)
                {
                    errors.Add(nameof(PurchaseData), 
                        new string[] { "There should be atleast one Input" });
                }

                if(request.GrossAmount >0 && (request.NetAmount >0 || request.VATAmount >0))
                {
                    errors.Add(nameof(request.GrossAmount),
                        new string[] { "There should only one input" });
                }

                if (request.NetAmount > 0 && (request.GrossAmount > 0 || request.VATAmount > 0))
                {
                    errors.Add(nameof(request.NetAmount),
                        new string[] { "There should only one input" });
                }

                if (request.VATAmount > 0 && (request.NetAmount > 0 || request.GrossAmount > 0))
                {
                    errors.Add(nameof(request.VATAmount),
                        new string[] { "There should only one input" });
                }

                if(errors.Count>0)
                    throw new ValidationException(errors);
            }
        }
    }
}
