using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProductArticleRepository : IRepository<ProductArticle>
    {
        Task AddProductArticle(List<int> articleId , int productId, CancellationToken cancellationToken);

        //متد زیر فعلا بلا استفاده س
        List<ProductArticle> GetListProductArticleByProductId(int productId);
        List<int> GetListArticleByProductId(int productId);
    }
}

