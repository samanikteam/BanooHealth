using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Comments
{
    public class AnswersModel : PageModel
    {

        private readonly ICommentRepository _CommentRepository;
        private readonly IArticleRepasitory _ArticleRepasitory;

        public AnswersModel(ICommentRepository commentRepository, IArticleRepasitory articleRepasitory)
        {
            _CommentRepository = commentRepository;
            _ArticleRepasitory = articleRepasitory;
        }

        [BindProperty]
        public CommentDto Commentdto { get; set; }
        public ListCommentDto listComment { get; set; }
        public ListCommentDto listAnswerComment { get; set; }


        public void OnGet(int id , int articleId , string email)
        {

             listComment = _CommentRepository.GetListCommentByEmailForOneArticle(id , email);
            listAnswerComment = _CommentRepository.GetListAnswerCommentByEmailForOneArticle(id , articleId, email);
            ViewData["id"]=id;
            ViewData["articleId"]= articleId;
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
