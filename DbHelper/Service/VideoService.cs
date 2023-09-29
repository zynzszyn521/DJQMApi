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
        private readonly VideoRepository _videoRepository;

        public VideoService(VideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public Task<ReturnResult> SaveVideo(VideoModel model)
        {
            return _videoRepository.SaveVideo(model);
        }
        public Task<ReturnResult> SaveVideoDetail(VideoDetailModel model)
        {
            return _videoRepository.SaveVideoDetail(model);
        }
        public Task<dynamic> GetVideoList(string userCode)
        {
            return _videoRepository.GetVideoList(userCode);
        }
        public Task<dynamic> GetVideoDetailList(string userCode, string videoId)
        {
            return _videoRepository.GetVideoDetailList(userCode, videoId);
        }
        public Task<dynamic> GetVideoDetailUrl(string userCode, string videoDetailId)
        {
            return _videoRepository.GetVideoDetailUrl(userCode, videoDetailId);
        }
        public Task<ReturnResult> SaveVideoFilePath(string videoId, string fileType, string fileUrl)
        {
            return _videoRepository.SaveVideoFilePath(videoId, fileType, fileUrl);
        }
    }
}
