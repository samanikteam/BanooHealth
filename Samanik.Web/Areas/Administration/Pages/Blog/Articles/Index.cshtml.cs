using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly IArticleCategoryAssignRepository _CAssignRepository;

        public IndexModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, IArticleCategoryAssignRepository cAssignRepository)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CAssignRepository = cAssignRepository;
        }

        [BindProperty]
        public ArticleDto dto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        public void OnGet()
        {
            ViewData["ArticleCategories"] = new SelectList(_CRepasitory.GetArticleCategories().Where(a=>a.IsDelete==false), "Id", "Title");
            listArticleDto = _Repasitory.GetListArticle();


        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, List<int> ListCategoryId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var exist = await _Repasitory.IsExistArticle(dto.Title);
            if (exist)
            {
                ModelState.AddModelError("Title", "مجله با مشخصات وارد شده تکراری است.");
                return Page();
            }


            var RegisterUserId = "Admin";
            //var RegisterUserId = User.FindFirst("UserId").Value;
            var ArticleId = await _Repasitory.AddAsync(dto, RegisterUserId, Image, cancellationToken);

            await _CAssignRepository.AddAssign(ListCategoryId, ArticleId, cancellationToken);

            return Redirect("/Administration/Blog/Articles/Index");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _Repasitory.Active(id, cancellationToken);
            return Redirect("/Administration/Blog/Articles/Index");
        }
        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _Repasitory.Deactive(id, cancellationToken);
            return Redirect("/Administration/Blog/Articles/Index");
        }
    }
}
