﻿using Data.Contracts;
using Data.Models;
using Entities.Media;
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
    public class LoginSliderRepository:Repository<LoginSlider>,ILoginSlider
    {
        public LoginSliderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddLoginSlider(LoginSliderDto LoginsliderDto, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            LoginSlider Loginslider = new LoginSlider()
            {
                Title = LoginsliderDto.Title,
                AvatarAlt = LoginsliderDto.AvatarAlt,
                Sort = 0,
                Status = LoginsliderDto.Status,
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        Loginslider.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion

            await base.AddAsync(Loginslider, cancellationToken);
        }

        public async Task Disable(int id, CancellationToken cancellationToken)
        {
            var Loginslider = GetById(id);
            Loginslider.Disable();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Enable(int id, CancellationToken cancellationToken)
        {
            var Loginslider = GetById(id);
            Loginslider.Enable();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListLoginSliderDto GetListLoginSliderDto(int PageNum = 1, int PageSize = 0)
        {
            var Loginslider = Table.OrderByDescending(a => a.Id);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListLoginSliderDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = Loginslider.Count();
            list.PageCount = (int)Math.Ceiling(Loginslider.Count() / (double)take);


            list.LoginSliders = Loginslider.Select(t => new LoginSliderDto()
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                Sort = t.Sort,
                AvatarAlt = t.AvatarAlt,
                Avatar=t.Avatar
                
            }).OrderBy(a => a.Title).Skip(skip).Take(take).ToList();    

            return list;
        }

        public List<Slider> GetLoginSliders()
        {
            throw new NotImplementedException();
        }
    }
}
