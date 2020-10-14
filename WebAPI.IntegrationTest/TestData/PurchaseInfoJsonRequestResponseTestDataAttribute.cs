using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace WebAPI.IntegrationTest.TestData
{
    public class PurchaseInfoJsonRequestResponseTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] 
            {
                "{\"VATRate\": 10, \"NETAmount\": 459}",
                "{\"VATRate\": 10, \"NETAmount\": 459, \"GrossAmount\":504.90, \"VATAmount\":45.90}"
            };

            yield return new object[]
            {
                "{\"VATRate\": 13, \"GrossAmount\": 3870.25}",
                "{\"VATRate\": 13, \"NETAmount\": 3425, \"GrossAmount\":3870.25, \"VATAmount\":445.25}"
            };

            yield return new object[]
            {
                "{\"VATRate\": 20, \"VATAmount\": 53.40}",
                "{\"VATRate\": 20, \"NETAmount\": 267, \"GrossAmount\":320.40, \"VATAmount\":53.40}"
            };
        }
    }
}
