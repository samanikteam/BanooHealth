using Data.Contracts;
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
using VisitorManagment.Core.Convertors;

namespace Data.Repositories
{
    public class SloganRepository : Repository<Slogan>, ISloganRepository
    {
        public SloganRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var slogan = GetById(id);
            slogan.IsActive = true;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task AddSlogan(SloganDto sloganDto, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            Slogan slogan = new Slogan()
            {
                Title = sloganDto.Title,
                Description = sloganDto.Description,
                AvatarAlt = sloganDto.AvatarAlt,
                AvatarTitle = sloganDto.AvatarTitle,
                IsActive = sloganDto.IsActive,
                RegisterDate = DateTime.Now
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        slogan.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion

            await base.AddAsync(slogan, cancellationToken);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var slogan = GetById(id);
            slogan.IsActive = false;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListSloganDto GetListSlogans()
        {
            var slogans = Table.OrderByDescending(a => a.RegisterDate);
            var list = new ListSloganDto() { };

            list.Slogans = slogans.Select(t => new SloganDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Avatar = t.Avatar,
                AvatarTitle=t.AvatarTitle,
                AvatarAlt=t.AvatarAlt,
                IsActive=t.IsActive,
                RegisterDate = t.RegisterDate.ToShamsi(),

            }).ToList();

            return list;
        }

        public List<Slogan> GetSlogans()
        {
            return Table.ToList();
        }
    }
}
