using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Application.Models;
using TaxSystem.Application.PurchaseInfo.Commands;
using TaxSystem.WebAPI.Models;

namespace TaxSystem.WebAPI.Controllers
{
    public class PurchaseController : ApiBaseController
    {       
        // POST api/<PurchaseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseDataModel purchaseData)
        {
            return Ok(await Mediator.Send(new CalculatePurchaseCommand 
            {
                GrossAmount = purchaseData.GrossAmount,
                NetAmount = purchaseData.NetAmount,
                VATAmount = purchaseData.VATAmount,
                VATRate = purchaseData.VATRate
            }));
        }
               
    }
}
