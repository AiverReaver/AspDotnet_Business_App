using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessApp.API.Data;
using BusinessApp.API.Helpers;
using BusinessApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IBusinessRepository _repo;

        public PaymentsController(IConfiguration config, IBusinessRepository repo)
        {
            _config = config;
            _repo = repo;
        }

        [HttpGet("{userId}/pay")]
        public IActionResult Payment(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Dictionary<String, String> paytmParams = new Dictionary<String, String>();
            String merchantMid = _config.GetSection("PaytmSettings:MerchantId").Value;
            // Key in your staging and production MID available in your dashboard
            String merchantKey = _config.GetSection("PaytmSettings:SecretKey").Value;
            // Key in your staging and production merchant key available in your dashboard
            String orderId = $"BUSA{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            String channelId = "WEB";
            String custId = $"CUS{userId}";
            String txnAmount = "99";
            String website = "WEBSTAGING";
            // This is the staging value. Production value is available in your dashboard
            String industryTypeId = "Retail";
            // This is the staging value. Production value is available in your dashboard
            String callbackUrl = $"http://43590713.ngrok.io/api/payments/{userId}";
            paytmParams.Add("MID", merchantMid);
            paytmParams.Add("CHANNEL_ID", channelId);
            paytmParams.Add("WEBSITE", website);
            paytmParams.Add("CALLBACK_URL", callbackUrl);
            paytmParams.Add("CUST_ID", custId);
            paytmParams.Add("ORDER_ID", orderId);
            paytmParams.Add("INDUSTRY_TYPE_ID", industryTypeId);
            paytmParams.Add("TXN_AMOUNT", txnAmount);

            var checksumHash = paytm.CheckSum.generateCheckSum(merchantKey, paytmParams);
            paytmParams.Add("CHECKSUMHASH", checksumHash);

            return Ok(paytmParams);
        }

        [AllowAnonymous]
        [HttpPost("{userId}")]
        public async Task<IActionResult> PaytmCallBack(int userId,[FromForm]PaytmOrder paytmOrder)
        {
            var userFromRepo = await _repo.GetUser(userId);

            if (paytmOrder.STATUS == "TXN_SUCCESS")
            {
                userFromRepo.PaytmOrders.Add(paytmOrder);

                if (userFromRepo.ValidTill.CompareTo(DateTime.Now) > 0)
                {
                    userFromRepo.ValidTill = userFromRepo.ValidTill.AddMonths(1);
                }
                else 
                {
                    userFromRepo.ValidTill = DateTime.Now.AddMonths(1);
                }

                if (await _repo.SaveAll())
                    return Redirect("http://localhost:5000");
            }

            return BadRequest("Something went Worng");
        }
    }
}