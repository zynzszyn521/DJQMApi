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
    public class ArticleRepository
    {
        private readonly DapperFactory _dapperFactory;

        public ArticleRepository(DapperFactory dapperFactory)
        {
            _dapperFactory = dapperFactory;
        }

        public async Task<ReturnResult> SaveArticle(ArticleModel model)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                model.articleId = strGuid;
                connection.Open();
                string strSql = " insert into djqm.Article(articleId,title,contentDesc,recommendFlag,content,userCode) values(@articleId,@title,@contentDesc,@recommendFlag,@content,@userCode) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = strGuid
                };
            }
        }
        public async Task<dynamic> GetArticleList(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select articleId,price,title,contentDesc,picUrl,recommendFlag,author,viewTimes,createTime from djqm.Article order by createTime desc ";
                return await connection.QueryAsync<ArticleModel>(strSql, new { }).ConfigureAwait(false);
            }
        }
        public async Task<dynamic> GetArticleDetail(string userCode, string articleId)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select * from djqm.Article where articleId=@articleId ";
                return await connection.QueryAsync<ArticleModel>(strSql, new { articleId = articleId }).ConfigureAwait(false);
            }
        }
        public async Task<ReturnResult> SaveArticleFilePath(string articleId, string fileType, string fileUrl)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                string strGuid = Guid.NewGuid().ToString();
                connection.Open();
                string strSql = string.Empty;
                if (fileType == "Image")
                {
                    strSql = " update djqm.Article set picUrl = @fileUrl where articleId=@articleId ";
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
                    articleId = articleId,
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
