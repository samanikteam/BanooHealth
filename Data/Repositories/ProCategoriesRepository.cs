using Data.Contracts;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProCategoriesRepository : Repository<ProCategory>, IProCategoriesRepository
    {
        public ProCategoriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddProductCategory(List<int> categoryId, int productId, CancellationToken cancellationToken)
        {
            await base.DeleteRangeAsync(Table.Where(a => a.ProductId == productId), cancellationToken);

            foreach (var item in categoryId)
            {
                var productCategory = new ProCategory
                {
                    ProductId = productId,
                    ProductCategoryId = item, 
                    SortNum=0
                };

                await base.AddAsync(productCategory, cancellationToken);
            }
        }

        public List<int> GetListProductIdWithProductCategoryId(int productCategoryId)
        {
            var result = Table.Where(x => x.ProductCategoryId == productCategoryId)
                .Select(x => x.ProductId).ToList();

            return result;
        }
    }
}
