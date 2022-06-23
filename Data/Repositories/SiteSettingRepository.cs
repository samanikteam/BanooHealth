using Data.Contracts;
using Data.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisitorManagment.Core.Generator;

namespace Data.Repositories
{
    public class SiteSettingRepository : Repository<SiteSetting>, ISiteSettingRepository
    {
        public SiteSettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public SiteSettingDto GetSetting()
        {
            var setting = Table.SingleOrDefault();

            var res = new SiteSettingDto()
            {
                Description = setting.Description,
                Keywords = setting.Keywords,
                Id = setting.Id,
                Paging = setting.Paging,
                SMTP = setting.SMTP,
                Title = setting.Title,
                FaviconAlt = setting.FaviconAlt,
                FaviconTitle = setting.FaviconTitle,
                LogoAlt = setting.LogoAlt,
                LogoTitle = setting.LogoTitle,
                FaviconShow = setting.Favicon,
                LogoShow = setting.Logo,
            };

            return res;
        }

        public async Task UpdateAsync(SiteSettingDto dto, CancellationToken cancellationToken)
        {
            var setting = await base.GetByIdAsync(cancellationToken, dto.Id);


            setting.Description = dto.Description;
            setting.Keywords = dto.Keywords;
            setting.Paging = dto.Paging;
            setting.SMTP = dto.SMTP;
            setting.Title = dto.Title;
            setting.FaviconAlt = dto.FaviconAlt;
            setting.FaviconTitle = dto.FaviconTitle;
            setting.LogoAlt = dto.LogoAlt;
            setting.LogoTitle = dto.LogoTitle;


            if (dto.Logo != null)
            {
                string imagePath = "";
                setting.Logo = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Logo.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", setting.Logo);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Logo.CopyTo(stream);
                }
            }

            if (dto.Favicon != null)
            {
                string imagePath = "";
                setting.Favicon = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Favicon.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", setting.Favicon);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Favicon.CopyTo(stream);
                }
            }

            await base.UpdateAsync(setting, cancellationToken);
        }
    }
}
