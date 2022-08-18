using Data.Contracts;
using Data.Models;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FilterRepository : Repository<Filter>, IFilterRepository
    {
        public FilterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddFilter(FilterDto filterDto, CancellationToken cancellationToken)
        {
            Filter filter = new Filter()
            {
                Status = true,
                Title = filterDto.Title
            };

            await base.AddAsync(filter, cancellationToken);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var filter = GetById(id);
            filter.Deactive();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var filter = GetById(id);
            filter.Active();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListFilterDto GetListFilter(int PageNum = 1)
        {
            var filters = Table;
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListFilterDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = filters.Count();
            list.PageCount = (int)Math.Ceiling(filters.Count() / (double)take);

            list.filters = filters.Select(t => new FilterDto()
            {
                Id=t.Id,
                Title = t.Title,
                Status = t.Status,
            }).OrderBy(t => t.Title).Skip(skip).Take(take).ToList(); 

            return list;
        }

        public List<Filter> Getfilters()
        {
            var result = Table.ToList();
            return result;
        }

        public void Edit(Filter filter)
        {
            var filter1 = Table.Where(x => x.Id == filter.Id).FirstOrDefault();
            filter1.Title = filter.Title;
            filter1.Status = true;
            DbContext.Update(filter1);
            DbContext.SaveChanges();
        }
    }
}
