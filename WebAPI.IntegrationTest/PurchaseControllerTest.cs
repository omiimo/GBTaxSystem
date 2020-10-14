using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxSystem.Application.Models;
using TaxSystem.Application.PurchaseInfo.Commands;
using TaxSystem.WebAPI;
using WebAPI.IntegrationTest.Common;
using WebAPI.IntegrationTest.TestData;
using Xunit;

namespace WebAPI.IntegrationTest
{
    public class PurchaseControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public PurchaseControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        [Theory]
        [PurchaseInfoJsonRequestResponseTestData]
        public async Task Should_GetSuccessJSONResponse_When_ValidJsonPurchaseInfoSupplied(string paymentInfoJson, string expectedResponseJson)
        {
            //Arrange
            var client = _factory.GetAnonymousClient();
            var apiEndPoint = "/api/purchase";
            var contentGrossAmount = Utilities.GetRequestContentFromJSONString(paymentInfoJson);
            PurchaseData expectedResponseObj = JsonConvert.DeserializeObject<PurchaseData>(expectedResponseJson);

            //Act
            var httpResponse = await client.PostAsync(apiEndPoint, contentGrossAmount);
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            PurchaseData responseObj = JsonConvert.DeserializeObject<PurchaseData>(responseString);

            // Assert - HTTP success response
            Assert.True(httpResponse.IsSuccessStatusCode );

            //Assert - compare httpresponse with expectedResponse
            Assert.Equal(expectedResponseObj.VATRate, responseObj.VATRate);
            Assert.Equal(expectedResponseObj.GrossAmount, responseObj.GrossAmount);
            Assert.Equal(expectedResponseObj.NetAmount, responseObj.NetAmount);
            Assert.Equal(expectedResponseObj.VATAmount, responseObj.VATAmount);

        }

        [Theory]
        [PurchaseInfoValidationTestData]
        public async Task Should_ValidatePurchaseInfoInputData_With_ExpectedResult(string paymentInfoJson, bool expectedResult)
        {
            //Arrange
            var client = _factory.GetAnonymousClient();
            var apiEndPoint = "/api/purchase";
            var contentGrossAmount = Utilities.GetRequestContentFromJSONString(paymentInfoJson);
            
            //Act
            var httpResponse = await client.PostAsync(apiEndPoint, contentGrossAmount);
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            // Assert - HTTP success response
            Assert.Equal(expectedResult, httpResponse.IsSuccessStatusCode);
            
        }
    }
}
