using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserNoteController : ControllerBase
    {
        private readonly ILogger<UserNoteController> _logger;
        private readonly UserNoteService _UserNoteService;

        public UserNoteController(ILogger<UserNoteController> logger, UserNoteService UserNoteService)
        {
            _logger = logger;
            _UserNoteService = UserNoteService;
        }

        [Route("SaveUserNote")]
        [HttpPost]
        public async Task<IActionResult> SaveUserNote(UserNoteModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _UserNoteService.SaveUserNote(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveUserNote:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetUserNote")]
        [HttpGet]
        public async Task<IActionResult> GetUserNote(string userCode,string appCode)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _UserNoteService.GetUserNote(userCode,appCode);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetUserNote:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("DeleteUserNote")]
        [HttpPost]
        public async Task<IActionResult> DeleteUserNote(UserNoteModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _UserNoteService.DeleteUserNote(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUserNote:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}