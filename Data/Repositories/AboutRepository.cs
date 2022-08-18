using Data.Contracts;
using Data.Migrations;
using Data.Models;
using Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AboutRepository : Repository<About>, IAboutRepository
    {
        public AboutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public AboutDto GetAbout()
        {
            var about = Table.FirstOrDefault();
            var res = new AboutDto()
            {
                Avatar = about.Avatar,
                Body = about.Body,
                AvatarAlt = about.AvatarAlt,
                AvatarTitle = about.AvatarTitle
            };
            return res;
        }

        public async Task UpdateAboutus(AboutDto aboutDto, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            //  var about = await base.GetByIdAsync(cancellationToken,aboutDto.Id);
            var about = Table.FirstOrDefault();
            about.Body = aboutDto.Body;
            about.AvatarTitle = aboutDto.AvatarTitle;
            about.AvatarAlt = aboutDto.AvatarAlt;

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        about.Avatar = stream.ToArray();
                    }
                }
            }

            await base.UpdateAsync(about, cancellationToken);
        }
    }
}
