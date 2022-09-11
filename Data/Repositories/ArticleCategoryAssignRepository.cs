using Data.Contracts;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ArticleCategoryAssignRepository:Repository<ArticleCategoryAssign>, IArticleCategoryAssignRepository
    {
        public ArticleCategoryAssignRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task AddAssign(List<int> categoryId, int articleId, CancellationToken cancellationToken)
        {
            await base.DeleteRangeAsync(Table.Where(a=>a.ArticleId==articleId),cancellationToken);

            foreach (var item in categoryId)
            {
                var articleCategory = new ArticleCategoryAssign
                {
                    ArticleId = articleId,
                    CategoryId = item,
                    SortNum = 0
                };
                await base.AddAsync(articleCategory, cancellationToken);
            }
        }


        public List<int> GetListArticleIdWithArticleCategoryId(int articleCategoryId)
        {
            var result = Table.Where(x => x.CategoryId == articleCategoryId)
                             .Select(x => x.ArticleId).ToList();

            return result;
        }

        public List<int> GetListArticleCategoryIdWithArticleId(int articleId)
        {
            var result = Table.Where(x => x.ArticleId == articleId).Select(x=>x.Id).ToList();
            return result;
        }
    }
}
