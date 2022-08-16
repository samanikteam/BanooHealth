using Data.Contracts;
using Data.Models;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
