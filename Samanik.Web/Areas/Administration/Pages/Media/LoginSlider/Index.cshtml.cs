using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Media.LoginSlider
{
    public class IndexModel : PageModel
    {
        private readonly ILoginSlider _loginSlider;

        public IndexModel(ILoginSlider loginSlider)
        {
            _loginSlider = loginSlider;
        }

        [BindProperty]
        public LoginSliderDto dto { get; set; }
        public ListLoginSliderDto ListSlider { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 12;
        public void OnGet(int PageNum = 1)
        {
            ListSlider = _loginSlider.GetListLoginSliderDto(PageNum,PageSize);
            //Add By vahid
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Administration/Media/LoginSlider/Index?PageNum=-");
                //Administration / Blog / Articles / Index
            }
            if (ListSlider.LoginSliders.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = ListSlider.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }
        }
        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _loginSlider.AddLoginSlider(dto, Image, cancellationToken);

            return Redirect("/Administration/Media/LoginSlider");
        }

        public async Task<IActionResult> OnPostEnable(int id, CancellationToken cancellationToken)
        {
            await _loginSlider.Enable(id, cancellationToken);
            return Redirect("/Administration/Media/LoginSlider");
        }

        public async Task<IActionResult> OnPostDisable(int id, CancellationToken cancellationToken)
        {
            await _loginSlider.Disable(id, cancellationToken);
            return Redirect("/Administration/Media/LoginSlider");
        }
    }
}
