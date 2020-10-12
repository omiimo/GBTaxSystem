using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using TaxSystem.Application.PurchaseInfo.Commands;
using Xunit;

namespace Application.UnitTests.PurchaseInfo
{
    public class PurchaseInfoTests
    {
        public PurchaseInfoTests()
        {

        }

        [Fact]
        public void ShouldHaveValidVATRate()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var pcmd = new CalculatePurchaseCommand.Handler();


            // Act
            pcmd.Handle(new CalculatePurchaseCommand { }, CancellationToken.None);

            // Assert
        }
    }
}
