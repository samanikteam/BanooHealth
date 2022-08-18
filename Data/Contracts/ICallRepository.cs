using Data.Models;
using Data.Repositories;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICallRepository : IRepository<Call>
    {
        Task AddAsync(CallDto Dto, CancellationToken cancellationToken);

        ListCallDto GetListCall();

        Task Confirm(int id,CancellationToken cancellationToken);
    }
}
