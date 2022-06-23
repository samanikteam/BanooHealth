using System;
using System.Collections.Generic;
using System.Linq;
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

        public void OnGet()
        {
            ListSlider = _loginSlider.GetListLoginSliderDto();
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
