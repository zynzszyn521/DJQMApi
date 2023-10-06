using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : ControllerBase
    {
        private readonly ILogger<SMSController> _logger;
        private readonly SMSService _smsService;

        public SMSController(ILogger<SMSController> logger, SMSService smsService)
        {
            _logger = logger;
            _smsService = smsService;
        }

        [Route("SendSMS")]
        [HttpPost]
        public async Task<IActionResult> SendSMS(SMSModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _smsService.SendSMS(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SendSMS:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}