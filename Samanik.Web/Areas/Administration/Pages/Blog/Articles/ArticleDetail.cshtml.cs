using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Articles
{
    public class ArticleDetailModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly IArticleCategoryAssignRepository _CAssignRepository;

        public ArticleDetailModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, IArticleCategoryAssignRepository cAssignRepository)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CAssignRepository = cAssignRepository;
        }

        [BindProperty]
        public ArticleDto articleDto { get; set; }
        public ListArticleDto listArticleDto { get; set; }

        public void OnGet(int id)
        {
            articleDto = _Repasitory.GetArticleById(id);
            listArticleDto = _Repasitory.GetListArticle();
            ViewData["ArticleCategories"] = new SelectList(_CRepasitory.GetArticleCategories(), "Id", "Title");

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken, List<int> ListCategoryId,List<IFormFile> Image)
        {
            if (!ModelState.IsValid)
                return Page();

            var RegisterUserId = "admin";
            var ArticleId = await _Repasitory.UpdateAsync(articleDto, RegisterUserId, Image, cancellationToken);

            if(ListCategoryId.Count!=0)
                await _CAssignRepository.AddAssign(ListCategoryId, ArticleId, cancellationToken);

            return Redirect("/Administration/Blog/Articles/ArticleDetail?id="+articleDto.Id);
        }
    }
}
