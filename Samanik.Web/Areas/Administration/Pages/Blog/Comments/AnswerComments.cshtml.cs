using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Comments
{
    public class AnswerCommentsModel : PageModel
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IArticleRepasitory _ArticleRepasitory;

        public AnswerCommentsModel(ICommentRepository commentRepository, IArticleRepasitory articleRepasitory)
        {
            _CommentRepository = commentRepository;
            _ArticleRepasitory = articleRepasitory;
        }

        [BindProperty]
        public CommentDto dto { get; set; }
        public ListCommentDto ListComment { get; set; }
        #region صفحه بندی
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        #endregion

        public void OnGet(int PageNum = 1)
        {
            ListComment = _CommentRepository.GetListAnswerComments(PageNum);
            #region صفحه بندی
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Administration/Blog/Comments/answercomments?PageNum=-");
                //Administration / Blog / Articles / Index
            }
            if (ListComment.Comments.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = ListComment.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }
            #endregion

        }
    }
}
