using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Entities.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Comments
{
    [Authorize]
    public class AnswerCommentsModel : PageModel
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IArticleRepasitory _ArticleRepasitory;
        private readonly IAuthorizationService _authorizationService;

        public AnswerCommentsModel(ICommentRepository commentRepository, IArticleRepasitory articleRepasitory, IAuthorizationService authorizationService)
        {
            _CommentRepository = commentRepository;
            _ArticleRepasitory = articleRepasitory;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public CommentDto dto { get; set; }
        public ListCommentDto ListComment { get; set; }
        #region صفحه بندی
        public PagingData PagingData { get; set; }
        public int PageSize = 12;
        #endregion

        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Blogs).Result.Succeeded)
            {
                ListComment = _CommentRepository.GetListAnswerComments(PageNum, PageSize);
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
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
          
        }
    }
}
