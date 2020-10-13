using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TaxSystem.Application.Models;
using Xunit.Sdk;

namespace Application.UnitTests.TestData
{
    /// <summary>
    /// A set of PurchaseInfo data as Input and its expected test result.
    /// </summary>
    public class PurchaseInfoInputTestDataAttribute : DataAttribute
    {        
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { new PurchaseData { VATRate = 10, GrossAmount = 0, NetAmount = 0,  VATAmount = 0 }, false };
            yield return new object[] { new PurchaseData { VATRate = 0, GrossAmount = 0, NetAmount = 0, VATAmount = 0 }, false };
            yield return new object[] { new PurchaseData { VATRate = 1, GrossAmount = 0, NetAmount = 0, VATAmount = 0 }, false };
            yield return new object[] { new PurchaseData { VATRate = 10, GrossAmount = 130, NetAmount = null, VATAmount = null }, true };
            yield return new object[] { new PurchaseData { VATRate = 10, NetAmount = 270}, true };
            yield return new object[] { new PurchaseData { VATRate = 20, NetAmount = 5698 }, true };

        }
    }
}
