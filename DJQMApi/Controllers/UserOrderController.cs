using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserOrderController : ControllerBase
    {
        private readonly ILogger<UserOrderController> _logger;
        private readonly UserOrderService _UserOrderService;

        public UserOrderController(ILogger<UserOrderController> logger, UserOrderService UserOrderService)
        {
            _logger = logger;
            _UserOrderService = UserOrderService;
        }

        [Route("SaveUserOrder")]
        [HttpPost]
        public async Task<IActionResult> SaveUserOrder(UserOrderModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _UserOrderService.SaveUserOrder(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveUserOrder:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetUserOrder")]
        [HttpGet]
        public async Task<IActionResult> GetUserOrder(string userCode, string orderType, string productId)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _UserOrderService.GetUserOrder(userCode, orderType, productId);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetUserOrder:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}