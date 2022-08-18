using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Pages.Blog
{
    public class ArticleByArticleCategoryModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _articleCategoryRepository;
        private readonly ICommentRepository _commnetRepository;

        public ArticleByArticleCategoryModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory articleCategoryRepository,
            ICommentRepository commnetRepository)
        {
            _Repasitory = repasitory;
            _articleCategoryRepository = articleCategoryRepository;
            _commnetRepository = commnetRepository;
        }

        [BindProperty]
        public ArticleDto dto { get; set; }
        public ArticleCategoryDto ArticleCategorydto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        public List<Category> listArticleCategoryDto { get; set; }
        public ListCommentDto commentDto { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        public void OnGet(int articleCategoryId , string slug, int PageNum = 1)
        {
            ViewData["ArticleCategories"] = new SelectList(_articleCategoryRepository.GetArticleCategories(), "Id", "Title");
            listArticleCategoryDto = _articleCategoryRepository.GetArticleCategories();
            listArticleDto = _Repasitory.GetListArticle(PageNum);
            commentDto = _commnetRepository.GetListComments(PageNum);
            ArticleCategorydto = _articleCategoryRepository.GetarticleCategorybyId(articleCategoryId);
            listArticleDto = _Repasitory.GetListArticlesByArticleCategoryId(articleCategoryId,PageNum);
            listArticleCategoryDto = _articleCategoryRepository.GetArticleCategories();

            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Blog/Category/"+ articleCategoryId +" ?PageNum=-");

            }
            if (listArticleDto.Articles.Count >= 0 )
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = listArticleDto.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 6
                };
            }
        }
    }
}
