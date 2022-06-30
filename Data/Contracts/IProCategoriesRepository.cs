using Data.Repositories;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProCategoriesRepository:IRepository<ProCategory>
    {
        Task AddProductCategory(List<int> categoryId, int productId, CancellationToken cancellationToken);
        List<int> GetListProductIdWithProductCategoryId(int productCategoryId);
        List<int> GetListCategoryWithProductId(int productId);

    }
}
