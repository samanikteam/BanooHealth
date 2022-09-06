using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Samanik.Web.Areas.Administration.Pages.Product.Comments
{
    public class AnswerCommentsModel : PageModel
    {
        private readonly IProCommentRepository _CommentRepository;

        public AnswerCommentsModel(IProCommentRepository CommentRepository)
        {
            _CommentRepository = CommentRepository;
        }

        [BindProperty]
        public ProCommentDto dto { get; set; }
        public ListProCommentDto ListComment { get; set; }
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
            if (ListComment.ProComments.Count >= 0)
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
