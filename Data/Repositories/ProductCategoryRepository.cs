using Data.Contracts;
using Data.Models;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class ProductCategoryRepository: Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var category = GetById(id);
            category.Active();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task AddProductCategory(ProductCategoryDto ProductCategoryDto, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            ProductCategory pCategory = new ProductCategory()
            {
                Title = ProductCategoryDto.Title,
                Description = ProductCategoryDto.Description,
                AvatarAlt = ProductCategoryDto.AvatarAlt,
                AvatarTitle = ProductCategoryDto.AvatarTitle,
                Slug = ProductCategoryDto.Slug,
                ParentId = ProductCategoryDto.ParentId,
                RegisterDate = DateTime.Now,
                IsDelete = false,
                Keywords=ProductCategoryDto.Keywords
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        pCategory.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion


            await base.AddAsync(pCategory, cancellationToken);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var category = GetById(id);
            category.Deactive();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ListProductCategoryDto GetListProductCategory(int PageNum = 1)
        {
            var productCategory = Table;
            var take = 8;
            var skip = (PageNum - 1) * take;
            var list = new ListProductCategoryDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = productCategory.Count();
            list.PageCount = (int)Math.Ceiling(productCategory.Count() / (double)take);

            list.ProductCategories = productCategory.Select(t => new ProductCategoryDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ParentId = t.ParentId,
                Avatar = t.Avatar,
                RegisterDate = t.RegisterDate,
                IsDelete = t.IsDelete,
                Slug=t.Slug,
                RegisterDateFa=t.RegisterDate.ToShamsi(),
                Keywords=t.Keywords
            }).OrderBy(c => c.Title).Skip(skip).Take(take).ToList();

            return list;
        }



        public List<ProductCategory> GetProductCategories()
        {
            return Table.ToList();
        }

        public async Task<bool> IsExistProductCategory(string title, int id)
        {
            return await TableNoTracking.AnyAsync(p => p.Title == title && p.ParentId == id);
        }

        public ListProductCategoryDto GetListProductCategoryByCategoryId(int categoryId)
        {
            var productCategory = Table.Where(x=>x.ParentId== categoryId);
            var list = new ListProductCategoryDto() { };

            list.ProductCategories = productCategory.Select(t => new ProductCategoryDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ParentId = t.ParentId,
                Avatar = t.Avatar,
                RegisterDate = t.RegisterDate,
                IsDelete = t.IsDelete,
                Keywords=t.Keywords
            }).ToList();

            return list;
        }

        public ProductCategoryDto GetProductCategorybyId(int id)
        {
            var result = GetById(id);

            var Productcategory = new ProductCategoryDto()
            {
                AvatarAlt = result.AvatarAlt,
                AvatarTitle = result.AvatarTitle,
                Description = result.Description,
                IsDelete = result.IsDelete,
                Slug = result.Slug,
                Title = result.Title,
                Avatar = result.Avatar,
                ParentId = result.ParentId,
                Id = result.Id,
                Keywords=result.Keywords
            };
            return Productcategory;
        }

        public async Task UpdateCategory(ProductCategoryDto Dto,List<IFormFile> Image, CancellationToken cancellationToken)
        {
            var ProductCategory = await base.GetByIdAsync(cancellationToken, Dto.Id);

            ProductCategory.Title = Dto.Title;
            ProductCategory.Description = Dto.Description;
            ProductCategory.AvatarAlt = Dto.AvatarAlt;
            ProductCategory.AvatarTitle = Dto.AvatarTitle;
            ProductCategory.Slug = Dto.Slug;
            ProductCategory.ParentId = Dto.ParentId;
            ProductCategory.IsDelete = Dto.IsDelete;
            ProductCategory.Keywords = Dto.Keywords;

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        ProductCategory.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion

            await base.UpdateAsync(ProductCategory, cancellationToken);
        }
    }
}
