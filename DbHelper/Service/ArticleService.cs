using DbHelper.Model;
using DbHelper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Service
{
    public class ArticleService
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<ReturnResult> SaveArticle(ArticleModel model)
        {
            return _articleRepository.SaveArticle(model);
        }
        public Task<dynamic> GetArticleList(string userCode)
        {
            return _articleRepository.GetArticleList(userCode);
        }
        public Task<dynamic> GetArticleDetail(string userCode,string articleId)
        {
            return _articleRepository.GetArticleDetail(userCode, articleId);
        }
    }
}
