using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Product.Comments
{
    public class AnswersModel : PageModel
    {
        private readonly IProCommentRepository _CommentRepository;

        public AnswersModel(IProCommentRepository CommentRepository)
        {
            _CommentRepository = CommentRepository;
        }

        [BindProperty]
        public ProCommentDto Commentdto { get; set; }
        public ListProCommentDto listComment { get; set; }
        public ListProCommentDto listAnswerComment { get; set; }


        public void OnGet(int id, int productId, string email)
        {

            listComment = _CommentRepository.GetListProCommentByEmailForOneProduct(id, email);
            listAnswerComment = _CommentRepository.GetListAnswerProCommentByEmailForOneProduct(id, productId, email);
            ViewData["id"] = id;
            ViewData["productId"] = productId;
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
