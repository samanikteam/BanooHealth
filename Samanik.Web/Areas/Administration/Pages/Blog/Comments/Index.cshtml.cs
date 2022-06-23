using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IArticleRepasitory _ArticleRepasitory;

        public IndexModel(ICommentRepository commentRepository, IArticleRepasitory articleRepasitory)
        {
            _CommentRepository = commentRepository;
            _ArticleRepasitory = articleRepasitory;
        }

        [BindProperty]
        public CommentDto dto { get; set; }
        public ListCommentDto ListComment { get; set; }


        public void OnGet()
        {
            ListComment = _CommentRepository.GetListComments();
            ViewData["Articles"] = new SelectList(_ArticleRepasitory.GetArticlesForComment(), "Id", "Title");
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();
            await _CommentRepository.AddComment(dto, cancellationToken);
            return Redirect("/Administration/Blog/Comments");
        }

        public async Task<IActionResult> OnPostConfirm(int id, CancellationToken cancellationToken)
        {
            await _CommentRepository.Confirm(id,cancellationToken);
            return Redirect("/Administration/Blog/Comments/Index");
        }

        public async Task<IActionResult> OnPostCancel(int id, CancellationToken cancellationToken)
        {
            await _CommentRepository.Cancel(id,cancellationToken);
            return Redirect("/Administration/Blog/Comments/Index");
        }
    }
}
