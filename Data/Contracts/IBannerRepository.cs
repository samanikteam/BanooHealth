using Data.Models;
using Data.Repositories;
using Entities.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IBannerRepository: IRepository<Banner>
    {
        BannerDto GetBanner();
        Task UpdateAsync(BannerDto dto, CancellationToken cancellationToken);
    }
}
