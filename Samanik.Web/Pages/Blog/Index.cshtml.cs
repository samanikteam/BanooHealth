using Data.Contracts;
using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly ICommentRepository _commnetRepository;

        public IndexModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, ICommentRepository commnetRepository)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _commnetRepository = commnetRepository;
        }

        [BindProperty]
        public ArticleDto dto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        public List<Category> listArticleCategoryDto { get; set; }
        public ListCommentDto commentDto { get; set; }


        public void OnGet()
        {
            ViewData["ArticleCategories"] = new SelectList(_CRepasitory.GetArticleCategories(), "Id", "Title");
            listArticleCategoryDto = _CRepasitory.GetArticleCategories();
            listArticleDto = _Repasitory.GetListArticle();
            commentDto = _commnetRepository.GetListComments();


        }
        public async Task<IActionResult> OnPost(List<IFormFile> Image , CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var exist = await _Repasitory.IsExistArticle(dto.Title);
            if (exist)
            {

                ModelState.AddModelError("Title", "مجله با مشخصات وارد شده تکراری است.");
                return Page();
            }
            //var RegisterUserId = User.FindFirst("UserId").Value;
            var RegisterUserId = "Admin";
            await _Repasitory.AddAsync(dto, RegisterUserId, Image, cancellationToken);
            return Redirect("");
        }

        public IActionResult OnPostConfirm(int id)
        {
            return RedirectToPage("/Administration/Blog/Comments/Index");
        }

    }
}
