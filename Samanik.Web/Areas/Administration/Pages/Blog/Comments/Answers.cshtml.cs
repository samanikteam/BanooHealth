using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Comments
{
    [Authorize]
    public class AnswersModel : PageModel
    {

        private readonly ICommentRepository _CommentRepository;
        private readonly IArticleRepasitory _ArticleRepasitory;
        private readonly IAuthorizationService _authorizationService;

        public AnswersModel(ICommentRepository commentRepository, IArticleRepasitory articleRepasitory, IAuthorizationService authorizationService)
        {
            _CommentRepository = commentRepository;
            _ArticleRepasitory = articleRepasitory;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public CommentDto Commentdto { get; set; }
        public ListCommentDto listComment { get; set; }
        public ListCommentDto listAnswerComment { get; set; }


        public IActionResult OnGet(int id , int articleId , string email)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Blogs).Result.Succeeded)
            {
                listComment = _CommentRepository.GetListCommentByEmailForOneArticle(id, email);
                listAnswerComment = _CommentRepository.GetListAnswerCommentByEmailForOneArticle(id, articleId, email);
                ViewData["id"] = id;
                ViewData["articleId"] = articleId;
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
           
        }


        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {

            await _CommentRepository.AnswerComment(Commentdto, cancellationToken);
           
            return Redirect("/Administration/Blog/Comments/answercomments?PageNum=1");
        }

     
        public async Task<IActionResult> OnPostCancel(int id, CancellationToken cancellationToken)
        {
            await _CommentRepository.Cancel(id, cancellationToken);
            return Redirect("/Administration/Blog/Comments/answercomments?PageNum=1");
        }
    }
}
