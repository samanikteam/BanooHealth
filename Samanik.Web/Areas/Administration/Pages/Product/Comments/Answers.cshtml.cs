using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Product.Comments
{
    [Authorize]
    public class AnswersModel : PageModel
    {
        private readonly IProCommentRepository _CommentRepository;
        private readonly IAuthorizationService _authorizationService;


        public AnswersModel(IProCommentRepository CommentRepository,IAuthorizationService authorizationService)
        {
            _CommentRepository = CommentRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProCommentDto Commentdto { get; set; }
        public ListProCommentDto listComment { get; set; }
        public ListProCommentDto listAnswerComment { get; set; }


        public IActionResult OnGet(int id, int productId, string email)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                listComment = _CommentRepository.GetListProCommentByEmailForOneProduct(id, email);
                listAnswerComment = _CommentRepository.GetListAnswerProCommentByEmailForOneProduct(id, productId, email);
                ViewData["id"] = id;
                ViewData["productId"] = productId;
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

            return Redirect("/Administration/Product/Comments/answercomments?PageNum=1");
        }


        public async Task<IActionResult> OnPostCancel(int id, CancellationToken cancellationToken)
        {
            await _CommentRepository.Cancel(id, cancellationToken);
            return Redirect("/Administration/Product/Comments/answercomments?PageNum=1");
        }
    }
}
