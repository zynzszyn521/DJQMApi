using Dapper;
using DbHelper.DbCon;
using DbHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Repository
{
    public class VideoRepository
    {
        private readonly DapperFactory _dapperFactory;

        public VideoRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveVideo(VideoModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                model.videoId = strGuid;
                connection.Open();
                string strSql = " insert into djqm.Video(videoId,title,contentDesc,recommendFlag,content,userCode) values(@videoId,@title,@contentDesc,@recommendFlag,@content,@userCode) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = strGuid
                };
            }
        }
        public async Task<ReturnResult> SaveVideoDetail(VideoDetailModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                model.videoId = strGuid;
                connection.Open();
                string strSql = " insert into djqm.VideoDetail(videoDetailId,videoDetailTitle,videoId,tryFlag,orderNo) values(@videoDetailId,@videoDetailTitle,@videoId,@tryFlag,@orderNo) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = strGuid
                };
            }
        }
        public async Task<dynamic> GetVideoList(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select videoId,price,title,contentDesc,picUrl,recommendFlag,author,viewTimes,createTime from djqm.Video order by createTime desc ";
                return await connection.QueryAsync<VideoModel>(strSql, new { }).ConfigureAwait(false);
            }
        }
        public async Task<dynamic> GetVideoDetailList(string userCode, string videoId)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select videoDetailId,videoDetailTitle,viewTimes,createTime,videoId,tryFlag,orderNo from djqm.VideoDetail where videoId=@videoId ";
                return await connection.QueryAsync<VideoDetailModel>(strSql, new { videoId = videoId }).ConfigureAwait(false);
            }
        }
        public async Task<dynamic> GetVideoDetailUrl(string userCode, string videoDetailId)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.VideoDetail where videoDetailId=@videoDetailId ";
                return await connection.QueryAsync<VideoDetailModel>(strSql, new { videoDetailId = videoDetailId }).ConfigureAwait(false);
            }
        }
        public async Task<ReturnResult> SaveVideoFilePath(string rowGuidId, string fileType, string fileUrl)
        {
            //图片上传的rowGuidId是videoId,视频上传的rowGuidId是videoDetailId
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                connection.Open();
                string strSql = string.Empty;
                if (fileType == "Image")
                {
                    strSql = " update djqm.Video set picUrl = @fileUrl where videoId=@rowGuidId ";
                }
                else if (fileType == "Video")
                {
                    strSql = " update djqm.VideoDetail set videoUrl = @fileUrl where videoDetailId=@rowGuidId ";
                }
                else
                {
                    return new ReturnResult()
                    {
                        successed = false,
                        msg = "Not Surpport " + fileType
                    };
                }
                int iReturn = await connection.ExecuteAsync(strSql, new
                {
                    rowGuidId = rowGuidId,
                    fileUrl = fileUrl
                }).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = strGuid
                };
            }
        }
    }
}
