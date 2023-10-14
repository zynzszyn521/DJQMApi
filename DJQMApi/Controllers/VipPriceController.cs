using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VipPriceController : ControllerBase
    {
        private readonly ILogger<VipPriceController> _logger;
        private readonly VipPriceService _VipPriceService;

        public VipPriceController(ILogger<VipPriceController> logger, VipPriceService VipPriceService)
        {
            _logger = logger;
            _VipPriceService = VipPriceService;
        }

        [Route("SaveVipPrice")]
        [HttpPost]
        public async Task<IActionResult> SaveVipPrice(VipPriceModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _VipPriceService.SaveVipPrice(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveVipPrice:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetVipPrice")]
        [HttpGet]
        public async Task<IActionResult> GetVipPrice(string userCode)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _VipPriceService.GetVipPrice(userCode);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVipPrice:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}