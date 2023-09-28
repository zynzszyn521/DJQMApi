using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class VideoService
    {
        private readonly VideoRepository _articleRepository;

        public VideoService(VideoRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<ReturnResult> SaveVideo(VideoModel model)
        {
            return _articleRepository.SaveVideo(model);
        }
        public Task<dynamic> GetVideoList(string userCode)
        {
            return _articleRepository.GetVideoList(userCode);
        }
        public Task<dynamic> GetVideoDetail(string userCode,string articleId)
        {
            return _articleRepository.GetVideoDetail(userCode, articleId);
        }
    }
}
