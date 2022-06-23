using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Categories
{
    public class EditModel : PageModel
    {
        private readonly IArticelCategoryRepasitory _articleCategoryRepasitory;
        private readonly IArticelCategoryRepasitory _Repository;

        public EditModel(IArticelCategoryRepasitory articleCategoryRepasitory, IArticelCategoryRepasitory repository)
        {
            _articleCategoryRepasitory = articleCategoryRepasitory;
            _Repository = repository;
        }

        [BindProperty]
        public ArticleCategoryDto dto { get; set; }

        public void OnGet(int id)
        {
            ViewData["ArticleCategories"] = new SelectList(_Repository.GetArticleCategories(), "Id", "Title");
            dto = _articleCategoryRepasitory.GetarticleCategorybyId(id);
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
