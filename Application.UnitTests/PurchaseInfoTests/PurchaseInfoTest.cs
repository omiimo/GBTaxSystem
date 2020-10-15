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
using TaxSystem.Application.Services;
using Xunit;

namespace Application.UnitTests.PurchaseInfo
{
    public class PurchaseInfoTest
    {
        /// <summary>
        /// Test will validate processed purchase data with supplied sample purchase data
        /// </summary>
        /// <param name="purchaseData">A Valid pre-calculated input purchase data</param>
        [Theory]
        [ValidPurchaseInfoTestData]
        public async void Should_CalculateAndComparePurchaseData_With_PreCalculatedPurchaseData(PurchaseData purchaseData)
        {
            // Arrange
            var pcmd = new CalculatePurchaseCommand.Handler(new PurchaseService());           

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
        /// Test should check for valid and invalid VAT rate (10, 13, 20) and compare with expectedResult.
        /// </summary>
        /// <param name="vatRate">VAT Rate</param>
        /// <param name="expectedResult">Input True if VAT Rate is a valid value (10, 13, 20) else false</param>
        [Theory]
        [InlineData(13, true)]
        [InlineData(10, true)]
        [InlineData(20, true)]
        [InlineData(50, false)]
        [InlineData(16, false)]
        public async void Should_ValidateInputVatRate_With_ExpectedResult(decimal vatRate, bool expectedResult)
        {

            // Arrange
            var pcmd = new CalculatePurchaseCommand.Handler(new PurchaseService());

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

        /// <summary>
        /// Validation test for input PurchaseData.
        /// </summary>
        /// <param name="purchaseData">Sample data for validation</param>
        /// <param name="expectedResult">Expected result for the purchaseData</param>
        [Theory]
        [PurchaseInfoInputTestData]
        public async void Should_ValidateInputPurchaseDataModel_With_ExpectedResult(PurchaseData purchaseData, bool expectedResult)
        {
            //Arrange
            var pcmd = new CalculatePurchaseCommand.Handler(new PurchaseService());

            //Act & Assert
            if (expectedResult == false)
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
