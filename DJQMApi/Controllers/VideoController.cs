using DbHelper.Model;
using DbHelper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DJQMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [Route("SaveVideo")]
        [HttpPost]
        public async Task<IActionResult> SaveVideo(VideoModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _videoService.SaveVideo(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveVideo:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        [Route("SaveVideoDetail")]
        [HttpPost]
        public async Task<IActionResult> SaveVideoDetail(VideoDetailModel model)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                returnResult = await _videoService.SaveVideoDetail(model);
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveVideoDetail:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        //获取视频标题清单
        [Route("GetVideoList")]
        [HttpGet]
        public async Task<IActionResult> GetVideoList(string userCode)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _videoService.GetVideoList(userCode);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVideoList:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        //获取视频子项清单
        [Route("GetVideoDetailList")]
        [HttpGet]
        public async Task<IActionResult> GetVideoDetailList(string userCode, string videoId)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _videoService.GetVideoDetailList(userCode, videoId);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVideoDetailList:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }
        //获取视频子项地址
        [Route("GetVideoDetailUrl")]
        [HttpGet]
        public async Task<IActionResult> GetVideoDetailUrl(string userCode, string videoDetailId)
        {
            IActionResult ihares;
            ReturnResult returnResult = new ReturnResult();
            try
            {
                var list = await _videoService.GetVideoDetailUrl(userCode, videoDetailId);
                returnResult = new ReturnResult()
                {
                    successed = true,
                    data = list
                };
                ihares = Ok(returnResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVideoDetailUrl:{0}", ex.Message);
                ihares = BadRequest(ex);
            }
            return await Task.FromResult(ihares);
        }

        [HttpPost]
        [Route("UploadVideoFile")]
        public async Task<IActionResult> UploadVideoFile()
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
                    _logger.LogError("UploadVideoFile Count:{0}", files.Count);
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
                        string strSaveUrl = Path.Combine("uploads", "Video", dic["guid"]);
                        string strFolderPath = Path.Combine("/home", "Video", strSaveUrl);
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
                        result = await _videoService.SaveVideoFilePath(dic["guid"], strFolderName, strSaveUrl);
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