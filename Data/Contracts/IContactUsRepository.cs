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
    public interface IContactUsRepository : IRepository<Contactus>
    {
        Task UpdateAsync(ContactusDto contactusDto, CancellationToken cancellationToken);
        ContactusDto GetContactus();
    }
}
