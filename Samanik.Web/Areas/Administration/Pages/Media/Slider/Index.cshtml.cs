using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Media.Slider
{
    public class IndexModel : PageModel
    {
        private readonly ISliderRepository _slider;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(ISliderRepository slider, IAuthorizationService authorizationService)
        {
            _slider = slider;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SliderDto dto { get; set; }
        public ListSliderDto ListSlider { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Resaneh).Result.Succeeded)
            {
                ListSlider = _slider.GetListSliderDto(PageNum);
                //Add By vahid
                StringBuilder QParam = new StringBuilder();
                if (PageNum != 0)
                {
                    QParam.Append($"/Administration/Media/Slider/Index?PageNum=-");
                    //Administration / Blog / Articles / Index
                }
                if (ListSlider.Sliders.Count >= 0)
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
                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
        }

        public async Task<IActionResult> OnPost( CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _slider.AddSlider(dto, cancellationToken);

            return Redirect("/Administration/Media/Slider");
        }

        public async Task<IActionResult> OnPostEnable(int id, CancellationToken cancellationToken)
        {
            await _slider.Enable(id, cancellationToken);
            return Redirect("/Administration/Media/Slider");
        }

        public async Task<IActionResult> OnPostDisable(int id, CancellationToken cancellationToken)
        {
            await _slider.Disable(id, cancellationToken);
            return Redirect("/Administration/Media/Slider");
        }
    }
}
