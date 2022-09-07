using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IArticelCategoryRepasitory _articleCategoryRepasitory;
        private readonly IArticelCategoryRepasitory _Repository;
        private readonly IAuthorizationService _authorizationService;


        public EditModel(IArticelCategoryRepasitory articleCategoryRepasitory, IArticelCategoryRepasitory repository, IAuthorizationService authorizationService)
        {
            _articleCategoryRepasitory = articleCategoryRepasitory;
            _Repository = repository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ArticleCategoryDto dto { get; set; }
        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Blogs).Result.Succeeded)
            {
                ViewData["ArticleCategories"] = new SelectList(_Repository.GetArticleCategories(), "Id", "Title");
                dto = _articleCategoryRepasitory.GetarticleCategorybyId(id);
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken, List<IFormFile> Image)
        {
            if (!ModelState.IsValid)
                return Page();

            var RegisterUserId = "admin";
            await _articleCategoryRepasitory.UpdateCategory(dto, RegisterUserId, Image, cancellationToken);
            return Redirect("/Administration/Blog/Categories");
        }
    }
}
