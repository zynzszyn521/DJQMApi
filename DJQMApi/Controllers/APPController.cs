using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APPController : ControllerBase
    {
        private readonly ILogger<APPController> _logger;
        private readonly APPService _appService;

        public APPController(ILogger<APPController> logger, APPService appService)
        {
            _logger = logger;
            _appService = appService;
        }

        [Route("GetAppServerVersion")]
        [HttpGet]
        public async Task<IActionResult> GetAppServerVersion(string appName)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _appService.GetAppServerVersion(appName);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAppServerVersion:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}