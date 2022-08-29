using Data.Contracts;
using Data.Models;
using Entities.Media;
using Microsoft.EntityFrameworkCore;
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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {

        public SliderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddSlider(SliderDto sliderDto, CancellationToken cancellationToken)
        {
            Slider slider = new Slider()
            {
                Title = sliderDto.Title,
                AvatarAlt=sliderDto.AvatarAlt,
                Link=sliderDto.Link,
                Sort=0,
                Status=sliderDto.Status,
            };

            if (sliderDto.Avatar != null)
            {
                string imagePath = "";
                slider.Avatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(sliderDto.Avatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/slider", slider.Avatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    sliderDto.Avatar.CopyTo(stream);
                }
            }

            await base.AddAsync(slider, cancellationToken);
        }

        public async Task Disable(int id, CancellationToken cancellationToken)
        {
            var slider = GetById(id);
            slider.Disable();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Enable(int id, CancellationToken cancellationToken)
        {
            var slider = GetById(id);
            slider.Enable();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListSliderDto GetListSliderDto(int PageNum = 1, int PageSize = 12)
        {
            var slider = Table.OrderByDescending(a=>a.Id);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListSliderDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = slider.Count();
            list.PageCount = (int)Math.Ceiling(slider.Count() / (double)take);

            list.Sliders = slider.Select(t => new SliderDto()
            {
                Id = t.Id,
                Title=t.Title,
                Link=t.Link,
                Status=t.Status,
                Sort=t.Sort,
                AvatarAlt=t.AvatarAlt,
                AvatarShow=t.Avatar
            }).OrderBy(a=>a.Title).Skip(skip).Take(take).ToList();  

            return list;
        }

        public SliderDto GetSliderById(int id)
        {
            var slider = GetById(id);


            var res = new SliderDto()
            {
                Id=slider.Id,
                Title = slider.Title,
                AvatarAlt = slider.AvatarAlt,
                Link = slider.Link,
                Sort = slider.Sort,
                Status = slider.Status,
                AvatarShow=slider.Avatar
            };

            return res;
        }

        public List<Slider> GetSliders()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SliderDto dto, CancellationToken cancellationToken)
        {
            var slider = await base.GetByIdAsync(cancellationToken, dto.Id);

            slider.Title = dto.Title;
            slider.Status = dto.Status;
            slider.Sort = dto.Sort;
            slider.Link = dto.Link;
            slider.AvatarAlt = dto.AvatarAlt;


            if (dto.Avatar != null)
            {
                string imagePath = "";
                slider.Avatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Media", slider.Avatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar.CopyTo(stream);
                }
            }

            await base.UpdateAsync(slider, cancellationToken);
        }
    }
}
