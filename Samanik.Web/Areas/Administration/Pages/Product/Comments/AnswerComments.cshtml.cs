using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Samanik.Web.Areas.Administration.Pages.Product.Comments
{
    [Authorize]
    public class AnswerCommentsModel : PageModel
    {
        private readonly IProCommentRepository _CommentRepository;
        private readonly IAuthorizationService _authorizationService;


        public AnswerCommentsModel(IProCommentRepository CommentRepository, IAuthorizationService authorizationService)
        {
            _CommentRepository = CommentRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProCommentDto dto { get; set; }
        public ListProCommentDto ListComment { get; set; }
        #region صفحه بندی
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        #endregion

        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
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
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
           

        }
    }
}
