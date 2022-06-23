using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IArticleRepasitory :  IRepository<Article>
    {
        Task<int> AddAsync(ArticleDto ArticleDto, string RegisterUserId,List<IFormFile> Image, CancellationToken cancellationToken);
        Task<int> UpdateAsync(ArticleDto ArticleDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken);
        Task<bool> IsExistArticle(string title);

        ListArticleDto GetListArticle();

        Task<Article> GetByIdAsync(CancellationToken cancellationToken, int id);
        ArticleDto GetArticleById(int id);
        ArticleDto GetArticleBySlug(string Slug);

        Article GetArticle(int id);
        List<Article> GetArticlesForComment();
      //  List<Article> GetListArticleById(int articleId);

        //List<ArticleDto> GetListArticleByProductId(int id);

        List<Article> GetListArticleFromTableProductArticleByArticleId(List<int> articleId);

        ProductDto GetListProductByArticleId(int id);

        List<Article> SearchArticleMainPage(string title);

        //search
        public List<Article> searchArticle( string title);


        Task Visited(int id, CancellationToken cancellationToken);

        ListArticleDto GetListArticlesByArticleCategoryId(int articleCategoryId);


    }
}
