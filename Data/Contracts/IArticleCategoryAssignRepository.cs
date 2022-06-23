using Data.Repositories;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IArticleCategoryAssignRepository : IRepository<ArticleCategoryAssign>
    {
        Task AddAssign(List<int> categoryId, int articleId, CancellationToken cancellationToken);
        List<int> GetListArticleIdWithArticleCategoryId(int articleCategoryId);

    }
}
