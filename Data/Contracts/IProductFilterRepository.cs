using Data.Models;
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
    public interface IProductFilterRepository:IRepository<ProductFilter>
    {
        Task AddProductFilter(ProductFilterDto Dto, CancellationToken cancellationToken);
        ListProductFilterDto GetListProductFilters(int id);
        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);
    }
}
