
using Data.Contracts;
using Data.Models;
using Entities.Articles;
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
    public class ArticleCategoryRepasitory : Repository<Category>, IArticelCategoryRepasitory
    {
        public ArticleCategoryRepasitory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddCategory(ArticleCategoryDto ArticleCategoryDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            Category articleCategory = new Category()
            {
                Title = ArticleCategoryDto.Title,
                Description = ArticleCategoryDto.Description,
                AvatarAlt = ArticleCategoryDto.AvatarAlt,
                AvatarTitle = ArticleCategoryDto.AvatarTitle,
                Slug = ArticleCategoryDto.Slug,
                ParentId = ArticleCategoryDto.ParentId,
                RegisterDate = DateTime.Now,
                RegisterUserId = RegisterUserId,
                IsDelete = false,
                Keywords=ArticleCategoryDto.keywords
            };

            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        articleCategory.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion


            await base.AddAsync(articleCategory, cancellationToken);
        }

        public List<Category> GetArticleCategories()
        {
            return Table.ToList();
        }

        public ListArticleCategoryDto GetListArticleCategory(int PageNum = 1,int PageSize=15)
        {
            var articleCategory = Table;
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListArticleCategoryDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = articleCategory.Count();
            list.PageCount = (int)Math.Ceiling(articleCategory.Count() / (double)take);
            list.articleCategories = articleCategory.Select(t => new ArticleCategoryDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ParentId = t.ParentId,
                Avatar = t.Avatar,
                RegisterDate = t.RegisterDate.ToShamsi(),
                IsDelete = t.IsDelete,
                Slug=t.Slug,
                keywords=t.Keywords
            }).OrderBy(u => u.Title).Skip(skip).Take(take).ToList();

            return list;
        }

        public async Task<bool> IsExistArticleCategory(string title, int id)
        {
            return await TableNoTracking.AnyAsync(p => p.Title == title && p.ParentId == id);
        }


        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var category = GetById(id);
            category.Active();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var category = GetById(id);
            category.Deactive();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ArticleCategoryDto GetarticleCategorybyId(int id)
        {
            var result = GetById(id);

            var articlecategory = new ArticleCategoryDto()
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
                keywords=result.Keywords
            };
            return articlecategory;
        }

        public async Task UpdateCategory(ArticleCategoryDto articlecategoryDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            var articleCategory = await base.GetByIdAsync(cancellationToken, articlecategoryDto.Id);

            articleCategory.Title = articlecategoryDto.Title;
            articleCategory.Description = articlecategoryDto.Description;
            articleCategory.AvatarAlt = articlecategoryDto.AvatarAlt;
            articleCategory.AvatarTitle = articlecategoryDto.AvatarTitle;
            articleCategory.Slug = articlecategoryDto.Slug;
            articleCategory.ParentId = articlecategoryDto.ParentId;
            articleCategory.LastUpdateDate = DateTime.Now;
            articleCategory.LastUpdateUserId = RegisterUserId;
            articleCategory.IsDelete = articlecategoryDto.IsDelete;
            articleCategory.Keywords = articlecategoryDto.keywords;


            #region Add Avatar(FileStream) in Model
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        articleCategory.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion

            await base.UpdateAsync(articleCategory, cancellationToken);

        }
    }
}
