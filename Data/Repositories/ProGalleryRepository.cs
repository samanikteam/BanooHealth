using Data.Contracts;
using Data.Models;
using Entities.Products;
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
    public class ProGalleryRepository : Repository<ProGallery>, IProGalleryRepository
    {
        private readonly IProductRepository _productRepository;
        public ProGalleryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task Activate(int id, CancellationToken cancellationToken)
        {
            var picture = Table.Where(a => a.Id == id).SingleOrDefault();
            if(picture.IsDelete==true)
            {
                picture.IsDelete = false;
            }
            else
            {
                picture.IsDelete = true;
            }
            
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> AddAsync(ProGalleryDto proGalleryDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            ProGallery proGallery = new ProGallery()
            {
                ProductId = proGalleryDto.ProductId,
                AvatarTitle = proGalleryDto.AvatarTitle,
                AvatarAlt = proGalleryDto.AvatarAlt,
                RegisterDate = DateTime.Now,
                RegisterUserId = RegisterUserId,
                IsDelete = false
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        proGallery.Avatar = stream.ToArray();
                    }
                }
            }


            #endregion

            await base.AddAsync(proGallery, cancellationToken);

            return proGallery.Id;
        }

        //لیست عکس های یه محصول براساس شناسه محصول 
        public ListProGalleryDto GetListPorGalleryByProductId(int productId)
        {
            var proGallery = Table.Where(a=>a.ProductId==productId).OrderByDescending(a => a.RegisterDate) ;
            var list = new ListProGalleryDto() { };
            list.ProGallery = proGallery.Select(t => new ProGalleryDto()
            {
                Id = t.Id,
                Avatar = t.Avatar,
                IsDelete=t.IsDelete

            }).ToList();

            return list;
        }
    }
}
