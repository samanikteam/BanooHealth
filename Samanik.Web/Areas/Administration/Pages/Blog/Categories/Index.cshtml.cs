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
    public class IndexModel : PageModel
    {
        private readonly IArticelCategoryRepasitory _Repository;


        public IndexModel(IArticelCategoryRepasitory cRepasitory)
        {
            _Repository = cRepasitory;

        }

        [BindProperty]
        public ArticleCategoryDto dto { get; set; }
        public ListArticleCategoryDto ListArticleCategoryDto { get; set; }

        public void OnGet()
        {
            ViewData["ArticleCategories"] = new SelectList(_Repository.GetArticleCategories(), "Id", "Title");
            ListArticleCategoryDto = _Repository.GetListArticleCategory();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var exist = await _Repository.IsExistArticleCategory(dto.Title, Convert.ToInt32(dto.ParentId));
            if (exist)
            {
                ViewData["ArticleCategories"] = new SelectList(_Repository.GetArticleCategories(), "Id", "Title");
                ModelState.AddModelError("Title", "دسته‌بندی با مشخصات وارد شده تکراری است.");
                return Page();
            }

            var RegisterUserId = "Admin";
            //var RegisterUserId = User.FindFirst("UserId").Value;
            await _Repository.AddCategory(dto, RegisterUserId, Image, cancellationToken);

            return Redirect("/Administration/Blog/Categories");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Active(id, cancellationToken);
            return Redirect("/Administration/Blog/Categories");
        }
        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Blog/Categories");
        }


    }
}

