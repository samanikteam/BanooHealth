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
    public interface IAboutRepository : IRepository<About>
    {
        Task UpdateAboutus(AboutDto aboutDto, List<IFormFile> Image, CancellationToken cancellationToken);

        AboutDto GetAbout();

    }
}
