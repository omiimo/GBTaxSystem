using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TaxSystem.Application.Models;
using Xunit.Sdk;

namespace Application.UnitTests.TestData
{
    /// <summary>
    /// A set of pre-calculated valid Purchase data as Input.
    /// </summary>
    public class ValidPurchaseInfoTestDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { new PurchaseData { VATRate = 10, GrossAmount = 504.90M, VATAmount = 45.90M, NetAmount=459 }};
            yield return new object[] { new PurchaseData { VATRate = 13, GrossAmount = 3870.25M, VATAmount = 445.25M, NetAmount = 3425 } };
            yield return new object[] { new PurchaseData { VATRate = 20, GrossAmount = 320.40M, VATAmount = 53.40M, NetAmount = 267 } };
        }
    }
}
