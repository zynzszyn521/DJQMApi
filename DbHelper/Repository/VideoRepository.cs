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
                connection.Open();
                string strSql = " insert into djqm.Video(videoId,title,content,userCode) values(@videoId,@title,@content,@userCode) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = "添加成功"
                };
            }
        }
        public async Task<dynamic> GetVideoList(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select videoId,price,title,author,viewTimes,createTime from djqm.Video ";
                return await connection.QueryAsync<VideoModel>(strSql, new { }).ConfigureAwait(false);
            }
        }
        public async Task<dynamic> GetVideoDetail(string userCode, string videoId)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.Video where videoId=@videoId ";
                return await connection.QueryAsync<VideoModel>(strSql, new { videoId = videoId }).ConfigureAwait(false);
            }
        }
    }
}
