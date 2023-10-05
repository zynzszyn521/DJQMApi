using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("SaveUser")]
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _userService.SaveUser(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveUser:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("UpdatePayUser")]
        [HttpPost]
        public async Task<IActionResult> UpdatePayUser(UserModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _userService.UpdatePayUser(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdatePayUser:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetUserInfo")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(string userCode)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _userService.GetUserById(userCode);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetUserById:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}