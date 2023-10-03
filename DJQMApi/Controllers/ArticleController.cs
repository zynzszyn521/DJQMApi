using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ArticleService _articleService;

        public ArticleController(ILogger<ArticleController> logger, ArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        [Route("SaveArticle")]
        [HttpPost]
        public async Task<IActionResult> SaveArticle(ArticleModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _articleService.SaveArticle(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveArticle:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetArticleList")]
        [HttpGet]
        public async Task<IActionResult> GetArticleList(string userCode)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _articleService.GetArticleList(userCode);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetArticleList:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("GetArticleDetail")]
        [HttpGet]
        public async Task<IActionResult> GetArticleDetail(string userCode, string articleId)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _articleService.GetArticleDetail(userCode, articleId);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetArticleDetail:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }

        [HttpPost]
        [Route("UploadArticleFile")]
        public async Task<IActionResult> UploadArticleFile()
        {
            IActionResult ihares;
            ReturnResult result = new ReturnResult();
            var files = HttpContext.Request.Form.Files;
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    dic.Add(key, HttpContext.Request.Form[key]);
                }
                if (files.Count > 0)
                {
                    _logger.LogError("UploadArticleFile Count:{0}", files.Count);
                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        string strFolderName = string.Empty;
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string extension = filename.Substring(filename.IndexOf("."));
                        switch (extension.ToLower())
                        {
                            case ".jpeg":
                            case ".jpg":
                            case ".bmp":
                            case ".png":
                            case ".gif":
                                strFolderName = "Image";
                                if (file.Length > 1024 * 1024 * 5)
                                {
                                    result = new ReturnResult() { successed = false, msg = "上传图片大小要小于5M" };
                                    return Ok(result);
                                }
                                break;
                            case ".mp4":
                                strFolderName = "Video";
                                if (file.Length > 1024 * 1024 * 100)
                                {
                                    result = new ReturnResult() { successed = false, msg = "上传视频大小要小于100M" };
                                    return Ok(result);
                                }
                                break;
                            default:
                                result = new ReturnResult()
                                {
                                    successed = false,
                                    msg = "Not Surpport"
                                };
                                return Ok(result);
                        }
                        //视频的图片素材和视频都存Video目录下的videoId目录
                        string strSaveUrl = Path.Combine("uploads", "Article", dic["guid"]);
                        string strFolderPath = Path.Combine("/home", "Article", strSaveUrl);
                        if (!Directory.Exists(strFolderPath))
                        {
                            Directory.CreateDirectory(strFolderPath);
                        }
                        string strFileName = Path.Combine(strFolderPath, dic["guid"] + filename);
                        strSaveUrl = Path.Combine(strSaveUrl, dic["guid"] + filename);
                        using (FileStream fs = System.IO.File.Create(strFileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        result = await _articleService.SaveArticleFilePath(dic["guid"], strFolderName, strSaveUrl);
                    }
                    ihares = Ok(result);
                }
                else
                {
                    ihares = BadRequest("Files length is 0");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("FileUpload:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
    }
}