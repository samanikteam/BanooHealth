using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Articles
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly IArticleCategoryAssignRepository _CAssignRepository;
        private readonly IAuthorizationService _authorizationService;
        public EditModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, IArticleCategoryAssignRepository cAssignRepository, IAuthorizationService authorizationService)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CAssignRepository = cAssignRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ArticleDto articleDto { get; set; }
        public ListArticleDto listArticleDto { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Blogs).Result.Succeeded)
            {
                articleDto = _Repasitory.GetArticleById(id);
                listArticleDto = _Repasitory.GetListArticle();

                ViewData["ArticleCategories"] = new SelectList(_CRepasitory.GetArticleCategories(), "Id", "Title");
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
            

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken, List<int> ListCategoryId, List<IFormFile> Image)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            var RegisterUserId = "admin";
            var ArticleId = await _Repasitory.UpdateAsync(articleDto, RegisterUserId, Image, cancellationToken);

            if (ListCategoryId.Count != 0)
                await _CAssignRepository.AddAssign(ListCategoryId, ArticleId, cancellationToken);

            return Redirect("/Administration/Blog/Articles/ArticleDetail?id=" + articleDto.Id);
        }
    }
}
