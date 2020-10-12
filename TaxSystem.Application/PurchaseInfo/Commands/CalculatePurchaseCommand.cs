using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxSystem.Application.Models;

namespace TaxSystem.Application.PurchaseInfo.Commands
{
    public class CalculatePurchaseCommand : IRequest<PurchaseData>
    {
        public decimal VATRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? VATAmount { get; set; }

        public class Handler : IRequestHandler<CalculatePurchaseCommand, PurchaseData>
        {
            public Task<PurchaseData> Handle(CalculatePurchaseCommand request, CancellationToken cancellationToken)
            {
                PurchaseData purchaseData = new PurchaseData
                {
                    GrossAmount = request.GrossAmount,
                    NetAmount = request.NetAmount,
                    VATAmount = request.VATAmount,
                    VATRate =  request.VATRate
                };

                var vatrate = purchaseData.VATRate / 100;
                if (purchaseData.VATAmount != null && purchaseData.VATAmount != 0)
                {
                    purchaseData.NetAmount = Math.Round((purchaseData.VATAmount.Value * (1 + vatrate)), 2);
                    purchaseData.GrossAmount = Math.Round((purchaseData.VATAmount.Value / vatrate), 2);
                    return Task.FromResult(purchaseData);
                }

                if (purchaseData.NetAmount != null && purchaseData.NetAmount != 0)
                {
                    purchaseData.VATAmount = Math.Round(purchaseData.NetAmount.Value * (vatrate) / (1 + vatrate), 2);
                    purchaseData.GrossAmount = Math.Round(purchaseData.NetAmount.Value / (1 + vatrate), 2);
                    return Task.FromResult(purchaseData);
                }
                if (purchaseData.GrossAmount != null && purchaseData.NetAmount != 0)
                {
                    purchaseData.NetAmount = Math.Round((purchaseData.GrossAmount.Value * (1 + vatrate)), 2);
                    purchaseData.VATAmount = Math.Round((purchaseData.GrossAmount.Value * vatrate), 2);
                    return Task.FromResult(purchaseData);
                }



                return Task.FromResult(purchaseData);
            }
        }
    }
}
