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
    public interface IFilterRepository :IRepository<Filter>
    {
        Task AddFilter(FilterDto filterDto, CancellationToken cancellationToken);
        ListFilterDto GetListFilter();
        List<Filter> Getfilters();
        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);
        void Edit(Entities.Products.Filter filter);
    }
}
