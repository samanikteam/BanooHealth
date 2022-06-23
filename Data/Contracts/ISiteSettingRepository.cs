using Data.Models;
using Data.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ISiteSettingRepository : IRepository<SiteSetting>
    {
        SiteSettingDto GetSetting();
        Task UpdateAsync(SiteSettingDto dto, CancellationToken cancellationToken);
    }
}
