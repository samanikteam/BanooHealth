using Data.Contracts;
using Data.Models;
using Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductArticleRepository : Repository<ProductArticle>, IProductArticleRepository
    {
        //private readonly IArticleRepasitory _articleRepository;
        public ProductArticleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddProductArticle(List<int> articleId, int productId, CancellationToken cancellationToken)
        {
            await base.DeleteRangeAsync(Table.Where(a => a.ProductId == productId), cancellationToken);

            foreach (var item in articleId)
            {
                var productArticle = new ProductArticle
                {
                    ProductId = productId,
                    ArticleId = item
                };

                await base.AddAsync(productArticle, cancellationToken);
            }
        }


        //متد زیر فعلا بلا استفاده س
        public List<ProductArticle> GetListProductArticleByProductId(int productId)
        {
            var result = Table.Where(x => x.ProductId == productId)
                .Include(x => x.Article).Include(x => x.Product)
                .ToList();

            return result;
        }


        public List<int> GetListArticleByProductId(int productId)
        {

            var result = Table.Where(x => x.ProductId == productId).Select(x => x.ArticleId).ToList();

            return result;

        }

        public ProductArticleDto GetListArticleAndProductByProductId(int productId)
        {
            var result = Table.Where(x => x.ProductId == productId).Include(x => x.Product).Include(x => x.Article);

            var listArticle = result.Select(x => x.Article).ToList();

            var listProduct = result.Where(x=>x.ProductId!=productId).Select(x => x.Product).ToList();

            ProductArticleDto productArticle = new ProductArticleDto();

            productArticle.ListArticle = listArticle;

            productArticle.ListProduct = listProduct;

            return productArticle;

        }
    }
}

