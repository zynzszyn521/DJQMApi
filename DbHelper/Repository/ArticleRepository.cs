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
                connection.Open();
                string strSql = " insert into djqm.Article(articleId,title,content,userCode) values(@articleId,@title,@content,@userCode) ";
                int iReturn = await connection.ExecuteAsync(strSql, model).ConfigureAwait(false);
                return new ReturnResult()
                {
                    successed = true,
                    msg = "添加成功"
                };
            }
        }
        public async Task<dynamic> GetArticleList(string userCode)
        {
            using (var connection = _dapperFactory.GetConnection())
            {
                connection.Open();
                string strSql = " select articleId,price,title,author,viewTimes,createTime from djqm.Article ";
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
    }
}
