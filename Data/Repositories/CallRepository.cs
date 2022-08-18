using Data.Contracts;
using Data.Models;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisitorManagment.Core.Convertors;

namespace Data.Repositories
{
    public class CallRepository : Repository<Call>, ICallRepository
    {

        public CallRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task AddAsync(CallDto Dto, CancellationToken cancellationToken)
        {
            Call call = new Call()
            {
                date = DateTime.Now,
                Email = Dto.Email,
                Message = Dto.Message,
                Name = Dto.Name,
                Title = Dto.Title,
                Status = false
            };
            await base.AddAsync(call, cancellationToken);
        }

        public async Task Confirm(int id, CancellationToken cancellationToken)
        {
            var call = GetById(id);
            call.Status = true;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListCallDto GetListCall()
        {
            var listcalls = Table.OrderByDescending(a=>a.date);
            var list = new ListCallDto() { };
            list.calls = listcalls.Select(t => new CallDto()
            {
                date = t.date.ToShamsi(),
                Email = t.Email,
                Message = t.Message,
                Name = t.Name,
                Status = t.Status,
                Title = t.Title,
                Id=t.Id
            }).ToList();

            return list;
        }
    }
}
