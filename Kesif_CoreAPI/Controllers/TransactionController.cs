using Kesif_CoreAPI.Models.ContextClasses;
using Kesif_CoreAPI.Models.Entities;
using Kesif_CoreAPI.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kesif_CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        MyContext _db;
        public TransactionController(MyContext db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> StartTransaction(PaymentRequestModel item)
        {
            if (_db.CardInfoes.Any(x => x.CardNumber == item.CardNumber && x.CCV == item.CCV && x.CardUserName == item.CardUserName)) //kart bilgileri tutuyorsa aslında burada daha ayrıntılı bir kontrol yapılır
            {

                UserCardInfo uCInfo = await _db.CardInfoes.SingleOrDefaultAsync(x => x.CardNumber == item.CardNumber && x.CCV == item.CCV && x.CardUserName == item.CardUserName);



                if (uCInfo.Balance >= item.ShoppingPrice)
                {
                    uCInfo.Balance -= item.ShoppingPrice; //Fiyat, kartın balance'indan düsüyor
                    await _db.SaveChangesAsync();

                    return Ok("Ödeme basarıyla alındı");
                }
                else return StatusCode(400, "Kart bakiyesi yetersiz bulundu");
            }

            return StatusCode(400, "Kart bilgileri yanlıs");
        }
    }
}


