using Data.Contracts;
using Data.Models;
using Entities.Media;
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
    public class BannerRepository: Repository<Banner>, IBannerRepository
    {
        public BannerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public BannerDto GetBanner()
        {
            var banner = Table.SingleOrDefault();
            if (banner != null)
            {
                var res = new BannerDto()
                {
                    Id = banner.Id,
                    Title1 = banner.Title1,
                    Title2 = banner.Title2,
                    Title3 = banner.Title3,
                    Title4 = banner.Title4,
                    Title5 = banner.Title5,
                    Title6 = banner.Title6,
                    Title7 = banner.Title7,
                    Title8 = banner.Title8,
                    Title9 = banner.Title9,
                    Link1 = banner.Link1,
                    Link2 = banner.Link2,
                    Link3 = banner.Link3,
                    Link4 = banner.Link4,
                    Link5 = banner.Link5,
                    Link6 = banner.Link6,
                    Link7 = banner.Link7,
                    Link8 = banner.Link8,
                    Link9 = banner.Link9,
                    AvatarShow1 = banner.Avatar1,
                    AvatarShow2 = banner.Avatar2,
                    AvatarShow3 = banner.Avatar3,
                    AvatarShow4 = banner.Avatar4,
                    AvatarShow5 = banner.Avatar5,
                    AvatarShow6 = banner.Avatar6,
                    AvatarShow7 = banner.Avatar7,
                    AvatarShow8 = banner.Avatar8,
                    AvatarShow9 = banner.Avatar9,
                };
                return res;
            }
            return null;
        }       

        public async Task UpdateAsync(BannerDto dto, CancellationToken cancellationToken)
        {
            var banner = await base.GetByIdAsync(cancellationToken, dto.Id);

            banner.Title1 = dto.Title1;
            banner.Title2 = dto.Title2;
            banner.Title3 = dto.Title3;
            banner.Title4 = dto.Title4;
            banner.Title5 = dto.Title5;
            banner.Title6 = dto.Title6;
            banner.Title7 = dto.Title7;
            banner.Title8 = dto.Title8;
            banner.Title9 = dto.Title9;


            banner.Link1 = dto.Link1;
            banner.Link2 = dto.Link2;
            banner.Link3 = dto.Link3;
            banner.Link4 = dto.Link4;
            banner.Link5 = dto.Link5;
            banner.Link6 = dto.Link6;
            banner.Link7 = dto.Link7;
            banner.Link8 = dto.Link8;
            banner.Link9 = dto.Link9;


            if (dto.Avatar1 != null)
            {
                string imagePath = "";
                banner.Avatar1 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar1.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar1);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar1.CopyTo(stream);
                }
            }
            if (dto.Avatar2 != null)
            {
                string imagePath = "";
                banner.Avatar2 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar2.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar2);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar2.CopyTo(stream);
                }
            }
            if (dto.Avatar3 != null)
            {
                string imagePath = "";
                banner.Avatar3 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar3.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar3);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar3.CopyTo(stream);
                }
            }

            if (dto.Avatar4 != null)
            {
                string imagePath = "";
                banner.Avatar4 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar4.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar4);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar4.CopyTo(stream);
                }
            }
            if (dto.Avatar5 != null)
            {
                string imagePath = "";
                banner.Avatar5 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar5.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar5);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar5.CopyTo(stream);
                }
            }
            if (dto.Avatar6 != null)
            {
                string imagePath = "";
                banner.Avatar6 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar6.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar6);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar6.CopyTo(stream);
                }
            }
            if (dto.Avatar7 != null)
            {
                string imagePath = "";
                banner.Avatar7 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar7.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar7);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar7.CopyTo(stream);
                }
            }
            if (dto.Avatar8 != null)
            {
                string imagePath = "";
                banner.Avatar8 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar8.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar8);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar8.CopyTo(stream);
                }
            }

            if (dto.Avatar9 != null)
            {
                string imagePath = "";
                banner.Avatar9 = NameGenerator.GenerateUniqCode() + Path.GetExtension(dto.Avatar9.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Them/assets/img/bg", banner.Avatar9);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    dto.Avatar9.CopyTo(stream);
                }
            }
            await base.UpdateAsync(banner, cancellationToken);
        }
    }
}
