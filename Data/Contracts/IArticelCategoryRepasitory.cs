using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IArticelCategoryRepasitory : IRepository<Category>
    {
        Task AddCategory(ArticleCategoryDto ArticleCategoryDto, string RegisterUserId,List<IFormFile> Image, CancellationToken cancellationToken);
        Task<bool> IsExistArticleCategory(string title, int id);
        List<Category> GetArticleCategories();
        ListArticleCategoryDto GetListArticleCategory(int PageNum = 1,int PageSize=12);

        ArticleCategoryDto GetarticleCategorybyId(int id);
        Task UpdateCategory(ArticleCategoryDto ArticleCategoryDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken);

        Task Active(int id, CancellationToken cancellationToken);
        Task Deactive(int id, CancellationToken cancellationToken);

    }
}
