using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using TaxSystem.Application.Exceptions;
using TaxSystem.Application.Models;
using TaxSystem.Application.PurchaseInfo.Commands;
using Xunit;

namespace Application.UnitTests.PurchaseInfo
{
    public class PurchaseInfoTests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vatRate">Valid VAT rates are 10,13,20</param>
        /// <param name="grossAmount">Gross Amount</param>
        /// <param name="vatAmount">Expected VAT Amount</param>
        /// <param name="netAmount">Expected Net Amount</param>
        [Theory]
        [InlineData(10, 459, 45.90, 504.90)]
        [InlineData(13, 3425, 445.25, 3870.25)]
        [InlineData(20, 267, 53.40, 320.40)]
        public async void CalculateValidPurchaseInfo(decimal vatRate, decimal grossAmount, decimal vatAmount, decimal netAmount)
        {
            // Arrange
            var pcmd = new CalculatePurchaseCommand.Handler();

            // Act
            var resultGrossAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate =  vatRate, GrossAmount = grossAmount }, CancellationToken.None);
            var resultVATAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, VATAmount = vatAmount }, CancellationToken.None);
            var resultNetAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, NetAmount = netAmount }, CancellationToken.None);

            // Assert
            Assert.Equal(vatRate, resultGrossAmount.VATRate);
            Assert.Equal(grossAmount, resultGrossAmount.GrossAmount);
            Assert.Equal(vatAmount, resultGrossAmount.VATAmount);
            Assert.Equal(netAmount, resultGrossAmount.NetAmount);

            Assert.Equal(vatRate, resultVATAmount.VATRate);
            Assert.Equal(grossAmount, resultVATAmount.GrossAmount);
            Assert.Equal(vatAmount, resultVATAmount.VATAmount);
            Assert.Equal(netAmount, resultVATAmount.NetAmount);

            Assert.Equal(vatRate, resultNetAmount.VATRate);
            Assert.Equal(grossAmount, resultNetAmount.GrossAmount);
            Assert.Equal(vatAmount, resultNetAmount.VATAmount);
            Assert.Equal(netAmount, resultNetAmount.NetAmount);

        }

        /// <summary>
        /// Test should paas for VAT rate 10, 13, 20.
        /// </summary>
        /// <param name="vatRate">VAT Rate</param>
        /// <param name="expectedResult">Input True if VAT Rate is a valid value (10, 13, 20) else false</param>
        [Theory]
        [InlineData(13,true)]
        [InlineData(10, true)]
        [InlineData(20, true)]
        [InlineData(50, false)]
        [InlineData(16, false)]
        public async void PurchaseDataVATRateValidation(decimal vatRate, bool expectedResult)
        {

            // Arrange
            var pcmd = new CalculatePurchaseCommand.Handler();

            //Act - assert
            if (expectedResult == false)
                await Assert.ThrowsAsync<ValidationException>(async () => await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, GrossAmount = 100 }, CancellationToken.None));
            else
            {
                var result = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, GrossAmount = 100 }, CancellationToken.None);
                Assert.IsType<PurchaseData>(result);
                Assert.Equal(vatRate, result.VATRate);
            }

        }

        [Theory]
        [InlineData(10,0,0,0,false)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(1, 0, 0, 0, false)]
        [InlineData(10, 130, 0, 0, true)]
        [InlineData(10, 0, 270, 0, true)]
        [InlineData(20, 0, 0, 5698, true)]
        public async void PurchaseInputDataValidation(decimal vatRate, decimal grossAmount, decimal netAmount, decimal vatAmount, bool expected)
        {
            //Arrange
            var pcmd = new CalculatePurchaseCommand.Handler();

            //Act
            if(expected == false)
            {
                await Assert.ThrowsAsync<ValidationException>(async () => await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, GrossAmount = grossAmount, NetAmount = netAmount, VATAmount = vatAmount }, CancellationToken.None));
            }
            else
            {
                var result = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = vatRate, GrossAmount = grossAmount, NetAmount = netAmount, VATAmount = vatAmount }, CancellationToken.None);
                Assert.IsType<PurchaseData>(result);
                Assert.Equal(vatRate, result.VATRate);
            }

            //Assert
        }
    }
}
