using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxSystem.Application.Models;
using TaxSystem.Application.PurchaseInfo.Commands;
using TaxSystem.WebAPI;
using WebAPI.IntegrationTest.Common;
using Xunit;

namespace WebAPI.IntegrationTest
{
    public class PurchaseInfo : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public PurchaseInfo(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Test for success result with multiple combination of input.
        /// </summary>
        /// <param name="vatRate">VAT Rate</param>
        /// <param name="grossAmount">Gross Amount</param>
        /// <param name="vatAmount">VAT Amount</param>
        /// <param name="netAmount">Net Amount</param>
        /// <returns></returns>
        [Theory]
        [InlineData(10, 504.90, 45.90, 459)]
        [InlineData(13, 3870.25, 445.25, 3425)]
        [InlineData(20, 320.40, 53.40, 267)]
        public async Task ReturnsSuccessResult(decimal vatRate, decimal grossAmount, decimal vatAmount, decimal netAmount)
        {
            //Arrange
            var client = _factory.GetAnonymousClient();
            var contentGrossAmount = Utilities.GetRequestContent(new CalculatePurchaseCommand { GrossAmount = grossAmount, VATRate = vatRate });
            var contentVATAmount = Utilities.GetRequestContent(new CalculatePurchaseCommand { VATAmount = vatAmount, VATRate = vatRate });
            var contentNetAmount = Utilities.GetRequestContent(new CalculatePurchaseCommand { NetAmount = netAmount, VATRate = vatRate });

            //Act
            var grossResponse = await client.PostAsync($"/api/purchase", contentGrossAmount);
            var vatResponse = await client.PostAsync($"/api/purchase", contentVATAmount);
            var netResponse = await client.PostAsync($"/api/purchase", contentNetAmount);

            PurchaseData responseGrossAmount = JsonConvert.DeserializeObject<PurchaseData>(await grossResponse.Content.ReadAsStringAsync());
            PurchaseData responseVATAmount = JsonConvert.DeserializeObject<PurchaseData>(await vatResponse.Content.ReadAsStringAsync());
            PurchaseData responseNetAmount = JsonConvert.DeserializeObject<PurchaseData>(await netResponse.Content.ReadAsStringAsync());

            // Assert
            Assert.True(grossResponse.IsSuccessStatusCode );
            Assert.True(vatResponse.IsSuccessStatusCode);
            Assert.True(netResponse.IsSuccessStatusCode);

            Assert.Equal(vatRate, responseGrossAmount.VATRate);
            Assert.Equal(grossAmount, responseGrossAmount.GrossAmount);
            Assert.Equal(vatAmount, responseGrossAmount.VATAmount);
            Assert.Equal(netAmount, responseGrossAmount.NetAmount);

            Assert.Equal(vatRate, responseVATAmount.VATRate);
            Assert.Equal(grossAmount, responseVATAmount.GrossAmount);
            Assert.Equal(vatAmount, responseVATAmount.VATAmount);
            Assert.Equal(netAmount, responseVATAmount.NetAmount);

            Assert.Equal(vatRate, responseNetAmount.VATRate);
            Assert.Equal(grossAmount, responseNetAmount.GrossAmount);
            Assert.Equal(vatAmount, responseNetAmount.VATAmount);
            Assert.Equal(netAmount, responseNetAmount.NetAmount);

        }
    }
}
