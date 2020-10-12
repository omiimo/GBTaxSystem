using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxSystem.Application.PurchaseInfo.Commands
{
    public class CalculatePurchaseCommandValidator : AbstractValidator<CalculatePurchaseCommand>
    {
        public CalculatePurchaseCommandValidator()
        {
            RuleFor(v => v.VATRate)
                .NotEmpty().WithMessage("vatRate is required")
                .Must(x => x == 10 || x == 13 || x == 20).WithMessage("vatRate should be 10, 13, 20");

            RuleFor(v => v.VATAmount)
                .GreaterThan(0).WithMessage("vatAmount should not be less than or equal to 0");

            RuleFor(v => v.GrossAmount)
                .GreaterThan(0).WithMessage("grossAmount should not be less than or equal to 0");

            RuleFor(v => v.NetAmount)
                .GreaterThan(0).WithMessage("netAmount should not be less than or equal to 0");

        }
    }
}
