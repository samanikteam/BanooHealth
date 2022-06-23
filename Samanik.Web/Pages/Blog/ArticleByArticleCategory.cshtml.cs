using System;
using System.Collections.Generic;
using System.Linq;
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
        public ListArticleDto listArticleDto { get; set; }
        public List<Category> listArticleCategoryDto { get; set; }
        public ListCommentDto commentDto { get; set; }
        public void OnGet(int articleCategoryId,string slug)
        {
            ViewData["ArticleCategories"] = new SelectList(_articleCategoryRepository.GetArticleCategories(), "Id", "Title");
            listArticleCategoryDto = _articleCategoryRepository.GetArticleCategories();
            listArticleDto = _Repasitory.GetListArticle();
            commentDto = _commnetRepository.GetListComments();

            listArticleDto = _Repasitory.GetListArticlesByArticleCategoryId(articleCategoryId);
            listArticleCategoryDto = _articleCategoryRepository.GetArticleCategories();
        }
    }
}
