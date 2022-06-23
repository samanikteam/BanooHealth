using Data.Models;
using Data.Repositories;
using Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ISloganRepository : IRepository<Slogan>
    {
        Task AddSlogan(SloganDto sloganDto, List<IFormFile> Image, CancellationToken cancellationToken);
        List<Slogan> GetSlogans();
        ListSloganDto GetListSlogans();
        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);

    }
}
