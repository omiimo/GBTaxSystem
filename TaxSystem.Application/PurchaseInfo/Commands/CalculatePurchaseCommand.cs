using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using TaxSystem.Application.Exceptions;
using TaxSystem.Application.Models;
using TaxSystem.Application.Services;

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
            IPurchaseService _purchaseService;
            public Handler(IPurchaseService purchaseService)
            {
                _purchaseService = purchaseService;
            }

            public async Task<PurchaseData> Handle(CalculatePurchaseCommand request, CancellationToken cancellationToken)
            {    
                return await _purchaseService.CalculatePurchaseInfo(new PurchaseData 
                {
                    GrossAmount = request.GrossAmount,
                    NetAmount = request.NetAmount,
                    VATAmount = request.VATAmount,
                    VATRate = request.VATRate
                });
            }
        }
    }
}
