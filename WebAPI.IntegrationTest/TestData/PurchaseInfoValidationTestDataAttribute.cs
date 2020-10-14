using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace WebAPI.IntegrationTest.TestData
{
    public class PurchaseInfoValidationTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[]
            {
                "{\"VATRate\": 10, \"NETAmount\": 459}",
                true
            };

            yield return new object[]
            {
                "{\"VATRate\": 13, \"GrossAmount\": 3870.25}",
                true
            };

            yield return new object[]
            {
                "{\"VATRate\": 13, \"NETAmount\": 454, \"GrossAmount\":null, \"VATAmount\":null}",
                true
            };

            yield return new object[]
            {
                "{\"VATRate\": 21, \"VATAmount\": 53.40}",
                false
            };

            yield return new object[]
            {
                "{\"VATRate\": 13, \"NETAmount\": 3425, \"GrossAmount\":0, \"VATAmount\":0}",
                false
            };

            yield return new object[]
            {
                "{\"VATRate\": 13, \"NETAmount\": 0}",
                false
            };

            yield return new object[]
            {
                "{\"VATRate\": 0, \"NETAmount\": 0, \"GrossAmount\":0, \"VATAmount\":0}",
                false
            };
        }
    }
}
