using Application.UnitTests.TestData;
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
        [ValidPurchaseInfoTestData]
        public async void CalculateValidPurchaseInfo(PurchaseData purchaseData)
        {
            // Arrange
            var pcmd = new CalculatePurchaseCommand.Handler();           

            // Act
            var resultGrossAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount }, CancellationToken.None);
            var resultVATAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = purchaseData.VATRate, VATAmount = purchaseData.VATAmount }, CancellationToken.None);
            var resultNetAmount = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = purchaseData.VATRate, NetAmount = purchaseData.NetAmount }, CancellationToken.None);

            // Assert
            Assert.Equal(purchaseData.VATRate, resultGrossAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultGrossAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultGrossAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultGrossAmount.NetAmount);

            Assert.Equal(purchaseData.VATRate, resultVATAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultVATAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultVATAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultVATAmount.NetAmount);

            Assert.Equal(purchaseData.VATRate, resultNetAmount.VATRate);
            Assert.Equal(purchaseData.GrossAmount, resultNetAmount.GrossAmount);
            Assert.Equal(purchaseData.VATAmount, resultNetAmount.VATAmount);
            Assert.Equal(purchaseData.NetAmount, resultNetAmount.NetAmount);

        }

        /// <summary>
        /// Test should paas for VAT rate 10, 13, 20.
        /// </summary>
        /// <param name="vatRate">VAT Rate</param>
        /// <param name="expectedResult">Input True if VAT Rate is a valid value (10, 13, 20) else false</param>
        [Theory]
        [InlineData(13, true)]
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
        [PurchaseInfoInputTestData]
        public async void PurchaseInputDataValidation(PurchaseData purchaseData, bool expected)
        {
            //Arrange
            var pcmd = new CalculatePurchaseCommand.Handler();

            //Act & Assert
            if (expected == false)
            {
                await Assert.ThrowsAsync<ValidationException>(async () => await pcmd.Handle(new CalculatePurchaseCommand { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount, NetAmount = purchaseData.NetAmount, VATAmount = purchaseData.VATAmount }, CancellationToken.None));
            }
            else
            {
                var result = await pcmd.Handle(new CalculatePurchaseCommand { VATRate = purchaseData.VATRate, GrossAmount = purchaseData.GrossAmount, NetAmount = purchaseData.NetAmount, VATAmount = purchaseData.VATAmount }, CancellationToken.None);
                Assert.IsType<PurchaseData>(result);
                Assert.Equal(purchaseData.VATRate, result.VATRate);
            }

        }
    }
}
