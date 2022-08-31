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
    public class ProductFilterRepository:Repository<ProductFilter> , IProductFilterRepository
    {
        public ProductFilterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var productFilter = GetById(id);
            productFilter.Confirm();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task AddProductFilter(ProductFilterDto Dto, CancellationToken cancellationToken)
        {
            ProductFilter productFilter = new ProductFilter()
            {
                Value = Dto.Value,
                FilterId = Dto.FilterId,
                ProductId = Dto.ProductId,
                Status = true,
            };

            await base.AddAsync(productFilter, cancellationToken);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var productFilter = GetById(id);
            productFilter.Cancel();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListProductFilterDto GetListProductFilters(int id)
        {
            var ProductFilters = Table.Include(x=>x.product).ThenInclude(x=>x.ProCategories).ThenInclude(x=>x.ProductCategory).Include(x => x.filter).Where(a => a.ProductId == id).OrderByDescending(x => x.Id);
            var list = new ListProductFilterDto() { };

            list.ProductFilters = ProductFilters.Select(t => new ProductFilterDto()
            {
                Id=t.Id,
                FilterId = t.FilterId,
                ProductId = t.ProductId,
                Status = t.Status,
                Value = t.Value,
                Filtername=t.filter.Title,
            }).ToList();



            return list;
        }
    }
}
